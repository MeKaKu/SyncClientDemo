using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

public enum PacketType{

    /// <summary>
    /// 包类型未初始化
    /// </summary>
    None = 0,

    /// <summary>
    /// 连接服务器成功
    /// </summary>
    ConnectSucceed = 1,

    /// <summary>
    /// 连接服务器失败
    /// </summary>
    ConnectFailed = 2,

    /// <summary>
    /// 收到新的TCP数据包
    /// </summary>
    TcpPacket = 3,

    /// <summary>
    /// 服务器断开
    /// </summary>
    Disconnected = 4

}

/// <summary>
/// 网络包定义
/// </summary>
public class NetPacket{
    public PacketType packetType = PacketType.None;
    public int protocolCode = 0;
    public int receivedCnt = 0;
    public byte[] packetHeaderBytes = null;
    public byte[] packetBodyBytes = null;
    public byte[] packetBytes = null;

    public static int HEADER_SIZE = 8;


    public NetPacket(PacketType _packetType){
        packetType = _packetType;
        protocolCode = 0;
        receivedCnt = 0;
    }
}

/// <summary>
/// 网络包安全队列
/// </summary>
public class PacketQueue{
    Queue<NetPacket> netPackets = new Queue<NetPacket>();
    public int Count{
        get{
            return netPackets.Count;
        }
    }
    public void Enqueue(NetPacket netPacket){
        lock(netPackets){
            netPackets.Enqueue(netPacket);
        }
    }

    public NetPacket Dequeue(){
        lock(netPackets){
            if(netPackets.Count > 0){
                return netPackets.Dequeue();
            }
            return null;
        }
    }
    
    public void Clear(){
        lock(netPackets){
            netPackets.Clear();
        }
    }
}

/// <summary>
/// TCP客户端
/// </summary>
public class TCPClient {
    /// <summary>
    /// 客户端socket
    /// </summary>
    Socket client = null;

    /// <summary>
    /// 推送给主线程的网络包队列
    /// </summary>
    PacketQueue packetQueue = new PacketQueue();

    /// <summary>
    /// 当前网络是否连接，true是已连接，false是未连接
    /// </summary>
    bool isConnected = false;
    public bool IsConnected {
        get{
            lock(this){
                return isConnected;
            }
        }
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="address"></param>
    /// <param name="port"></param>
    public void Connect(string address, int port){
        lock(this){
            if(isConnected == false){
                try{
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.BeginConnect(address, port, OnConnectCallBack, socket);
                }
                catch(Exception){
                    packetQueue.Enqueue(new NetPacket(PacketType.ConnectFailed));
                }
            }
        }
    }

    /// <summary>
    /// 获取全部的网络包
    /// </summary>
    /// <returns></returns>
    public List<NetPacket> GetAllPackets(){
        List<NetPacket> packetList = new List<NetPacket>();
        while(packetQueue.Count > 0){
            packetList.Add(packetQueue.Dequeue());
        }
        return packetList;
    }

    public void SendAsync(int protocolCode, byte[] bodyBuffer){
        byte[] codeBuffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protocolCode));
        byte[] bodyLengthBuffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(bodyBuffer.Length));
        byte[] packetBuffer = new byte[codeBuffer.Length + bodyLengthBuffer.Length + bodyBuffer.Length];
        Array.Copy(codeBuffer, 0, packetBuffer, 0, codeBuffer.Length);
        Array.Copy(bodyLengthBuffer, 0, packetBuffer, codeBuffer.Length, bodyLengthBuffer.Length);
        Array.Copy(bodyBuffer, 0, packetBuffer, codeBuffer.Length + bodyLengthBuffer.Length, bodyBuffer.Length);

        SendAsync(packetBuffer);
    }
    private void SendAsync(byte[] packetBuffer){
        lock(this){
            try{
                if(isConnected == true){
                    client.BeginSend(packetBuffer, 0, packetBuffer.Length, SocketFlags.None, OnSendCallBack, client);
                }
            }
            catch(Exception){
                Disconnect();
            }
        }
    }

    private void OnSendCallBack(IAsyncResult ar){
        lock(this){
            try{
                Socket socket = (Socket)ar.AsyncState;
                socket.EndSend(ar);
            }
            catch(Exception){
                Disconnect();
            }
        }
    }

    private void OnConnectCallBack(IAsyncResult ar){
        lock(this){
            if(isConnected == true){
                return;
            }
            try{
                client = (Socket)ar.AsyncState;
                isConnected = true;
                client.EndConnect(ar);
                packetQueue.Enqueue(new NetPacket(PacketType.ConnectSucceed));
                ReadPacket();
            }
            catch(Exception){
                client = null;
                isConnected = false;
                packetQueue.Enqueue(new NetPacket(PacketType.ConnectFailed));
            }
        }
    }

    public void ReceiveHeader(IAsyncResult ar){
        lock(this){
            try{
                NetPacket netPacket = (NetPacket)ar.AsyncState;

                //这次接收到的字节长度
                int readCnt = client.EndReceive(ar);

                //服务器主动断开
                if(readCnt == 0){
                    Disconnect();
                    return;
                }

                netPacket.receivedCnt += readCnt;
                if(netPacket.receivedCnt == NetPacket.HEADER_SIZE){//收到了指定的包头长度
                    netPacket.receivedCnt = 0;//开始接收包体了，已接收字节长度清零
                    //包体长度
                    int bodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(netPacket.packetHeaderBytes, 4));
                    //协议号
                    int protocolCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(netPacket.packetHeaderBytes, 0));
                    netPacket.protocolCode = protocolCode;

                    if(bodyLength < 0){//包体长度小于0
                        Disconnect();
                        return ;
                    }

                    netPacket.packetBodyBytes = new byte[bodyLength];

                    if(bodyLength == 0){
                        packetQueue.Enqueue(netPacket);
                        ReadPacket();
                        return ;
                    }

                    //开始接收包体数据
                    client.BeginReceive(netPacket.packetBodyBytes, 0, bodyLength, SocketFlags.None, ReceiveBody, netPacket);
                }
                else{//包头长度不够
                    //剩余需要接收的包头长度
                    int remainedCnt = NetPacket.HEADER_SIZE - netPacket.receivedCnt;
                    //继续接收包头
                    client.BeginReceive(netPacket.packetHeaderBytes, netPacket.receivedCnt, remainedCnt,SocketFlags.None, ReceiveHeader, netPacket);
                }
            }
            catch(Exception){
                Disconnect();
            }
        }
    }

    /// <summary>
    /// 接收包体数据
    /// </summary>
    /// <param name="ar"></param>
    public void ReceiveBody(IAsyncResult ar){
        lock(this){
            try{
                NetPacket netPacket = ar.AsyncState as NetPacket;
                //这次接收到的字节长度
                int readCnt = client.EndReceive(ar);

                //服务器主动断开
                if(readCnt == 0){
                    Disconnect();
                    return;
                }

                netPacket.receivedCnt += readCnt;

                if(netPacket.receivedCnt ==  netPacket.packetBodyBytes.Length){
                    netPacket.receivedCnt = 0;
                    packetQueue.Enqueue(netPacket);
                    ReadPacket();
                }
                else{
                    int remainedCnt = netPacket.packetBodyBytes.Length - netPacket.receivedCnt;
                    client.BeginReceive(netPacket.packetBodyBytes, netPacket.receivedCnt, remainedCnt, SocketFlags.None, ReceiveBody, netPacket);
                }
            }
            catch(Exception){
                Disconnect();
            }
        }
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Disconnect(){
        lock(this){
            if(isConnected == true){
                try{
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    client = null;
                    isConnected = false;
                    packetQueue.Enqueue(new NetPacket(PacketType.Disconnected));
                }
                catch(Exception){
                    client.Close();
                    client = null;
                    isConnected = false;
                    packetQueue.Clear();
                    packetQueue.Enqueue(new NetPacket(PacketType.Disconnected));
                }
            }
        }
    }

    private void ReadPacket(){
        NetPacket netPacket = new NetPacket(PacketType.TcpPacket);

        netPacket.packetHeaderBytes = new byte[NetPacket.HEADER_SIZE];

        client.BeginReceive(netPacket.packetHeaderBytes, 0, NetPacket.HEADER_SIZE, SocketFlags.None, ReceiveHeader, netPacket);
    }

}
