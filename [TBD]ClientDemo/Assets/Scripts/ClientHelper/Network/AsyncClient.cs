using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class AsyncClient
{
    TcpClient client;
    NetworkStream stream{
        get{
            return client.GetStream();
        }
    }
    const int BufferSize = 2048;

    public AsyncClient(){
        client = new TcpClient();
    }

    public void Connect(string ip, int port){
        //连接服务器
        Debug.Log("开始连接服务器...");
        client.Connect(ip, port);
        Debug.Log("连接服务器成功！");

        byte[] buffer = new byte[BufferSize];
        stream.BeginRead(buffer, 0, BufferSize, OnReceiveMsg, buffer);


    }

    private void OnReceiveMsg(IAsyncResult ar){
        int byteCount = stream.EndRead(ar);//获取接收到的字节数
        Debug.Log("收到的字节长度 = " + byteCount);
        if(byteCount > 0){
            var buffer = ar.AsyncState as byte[];
            int code = IPAddress.NetworkToHostOrder( BitConverter.ToInt32(buffer, 0) );
            int len = IPAddress.NetworkToHostOrder( BitConverter.ToInt32(buffer, 4) );
            //string msg = BitConverter.ToString(buffer, 8, len);
            string msg = Encoding.UTF8.GetString(buffer, 8, len);
            //string msg = Encoding.UTF8.GetString(buffer, 0, byteCount);

            Debug.Log($"收到来着服务器的消息: code = {code}, len = {len}, {msg}");

            buffer = new byte[BufferSize];
            stream.BeginRead(buffer, 0, BufferSize, OnReceiveMsg, null);
        }
        else{
            Debug.Log("连接断开....");  
        }
    }

    public void Send(string msg){

        byte[] content = Encoding.UTF8.GetBytes(msg);
        byte[] buffer = new byte[content.Length + 8];
        byte[] code = BitConverter.GetBytes(0);
        byte[] len = BitConverter.GetBytes(content.Length);
        Array.Copy(code, 0, buffer, 0, 4);
        Array.Copy(len, 0, buffer, 4, 4);
        Array.Copy(content, 0, buffer, 8, content.Length);

        Debug.Log($"准备向服务器发送消息：{msg}");
        stream.Write(buffer, 0, buffer.Length);
        Debug.Log("code = " + BitConverter.ToInt32(code, 0));
        Debug.Log( "长度 = " + BitConverter.ToInt32(len, 0));
        Debug.Log($"成功向服务器发送消息：{Encoding.UTF8.GetString(content)}");
    }

    public class Packet{
        public int code;
        public int length;

        object msg;
    }
}
