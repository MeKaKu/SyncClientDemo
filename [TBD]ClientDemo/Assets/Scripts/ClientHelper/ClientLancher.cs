using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;
public class NetData{
    public int protocolCode;
    //public object data;

    public string msg;

    public NetData(int protocolCode, string msg){
        this.protocolCode = protocolCode;
        this.msg = msg;
    }
}
public class ClientLancher : MonoBehaviour{
    public TCPClient tcpClient{
        get;
        private set;
    }
    //string ip = "10.21.197.43";
    string ip = "127.0.0.1";
    int port = 8080;
    public event Action<NetData> onTcpPacket;
    public event Action onConnectSucceed;
    public event Action onConnectFailed;
    public event Action onDisconnect;

    public static ClientLancher instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            tcpClient = new TCPClient();
            tcpClient.Connect(ip, port);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void Update(){
        if(tcpClient != null){
            var netPackets = tcpClient.GetAllPackets();
            foreach(var netPacket in netPackets){
                if(netPacket.packetType == PacketType.TcpPacket){
                    LT.Logging("接收:"+netPacket.protocolCode+":"+Encoding.UTF8.GetString(netPacket.packetBodyBytes));
                }
                else{
                    LT.Logging("Info:"+netPacket.packetType);
                }
                switch(netPacket.packetType){
                    case PacketType.TcpPacket:
                        NetData netData = new NetData(netPacket.protocolCode, Encoding.UTF8.GetString(netPacket.packetBodyBytes));
                        onTcpPacket?.Invoke(netData);
                        NetAdapter.Adapter(netData);
                    break;
                    case PacketType.ConnectSucceed:
                        onConnectSucceed?.Invoke();
                    break;
                    case PacketType.ConnectFailed:
                        onConnectFailed?.Invoke();
                    break;
                    case PacketType.Disconnected:
                        onDisconnect?.Invoke();
                    break;
                }
            }
        }
    }

    public bool SendMsg(Message msg){
        if(tcpClient != null && tcpClient.IsConnected){
            int protocolCode = msg.GetMessageType();
            string json = JsonHelper.SerializeObject(msg);
            Debug.Log("准备向服务器发送请求..."+ protocolCode + " : " + json);
            LT.Logging("请求:"+ protocolCode + ":" + json);
            tcpClient.SendAsync(protocolCode, Encoding.UTF8.GetBytes(json));
            return true;
        }
        return false;
    }
}
