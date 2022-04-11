using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.UI;
using Assets.Scripts.ClientHelper.Messages;
using Assets.Scripts.ClientHelper.Network;
using Assets.Scripts.ClientHelper.Entities;

public class TestTcpClient : Test
{
    private async void Start() {
        ClientLancher.instance.onConnectSucceed += () => {
            print("连接服务器成功");
        };
        ClientLancher.instance.onTcpPacket += (NetData netData) => {
            print($"收到了返回信息: code = {netData.protocolCode}, msg = {netData.msg}");
            HanderMsg(netData);
        };
        ClientLancher.instance.onConnectFailed += () => {
            print("连接失败");
        };
        ClientLancher.instance.onDisconnect += () => {
            print("断开连接");
        };

        await Task.Delay(TimeSpan.FromSeconds(1));
        Vector3 v = transform.position;
        Debug.Log(transform.position);
        Debug.Log(transform.position.ToString());
        Color color = LocalData.localData.menuPlane.GetComponentInChildren<Image>().color;
        Debug.Log(color.GetType());
        Dictionary<int, string> map = new Dictionary<int, string>();
        Debug.Log(JsonHelper.SerializeObject(map));
        map.Add(2,"AAA");
        string mapJson = JsonHelper.SerializeObject(map);
        Debug.Log(mapJson);
        map = JsonHelper.DeserializeAnonymousType(mapJson, map);
        Debug.Log(map[2]);
        //GameObject itemPrefab = Resources.Load<GameObject>("Img_Cube");
        //GameObject newItem = Instantiate<GameObject>(itemPrefab, GameObject.Find("SyncItems").transform);
        // Vector3 v3 = transform.position;
        // string json = JsonHelper.SerializeObject(v3);
        // Debug.Log("json = " + json);
        //ClientLancher.instance.tcpClient.Disconnect();
        //心跳
        //Invoke("HeartPump", 3);

        // NetAPI.LoginIn("1234", "1234");
        // NetAPI.onLoginResult = (_msg) => {
        //     Debug.Log("登陆成功" + _msg);
        // };

        // await Task.Delay(TimeSpan.FromSeconds(2));
        // NetRoom.CreateRoom("room1", (_msg) => {
        //    Debug.Log("创建房间成功" + _msg);
        // });

        //await Task.Delay(TimeSpan.FromSeconds(2));
        //ClientLancher.instance.SendMsg(((int)NetApiType.JoinRoom), new NetRoom.RoomInfo("room1"));
        //ClientLancher.instance.tcpClient.Disconnect();

        //await Task.Delay(TimeSpan.FromSeconds(2));
        //ClientLancher.instance.SendMsg(((int)NetApiType.RoomReady), new RoomReadyInfo(3));

        //await Task.Delay(TimeSpan.FromSeconds(2));
        //ClientLancher.instance.tcpClient.Disconnect();
    }

    void HeartPump(){
        ClientLancher.instance.SendMsg(new PumpMessage());
        Invoke("HeartPump", 3);
    }

    private void HanderMsg(NetData netData)
    {
        // if(netData.protocolCode == ((int)NetApiType.RoomReadyResult)){
        //     Debug.Log("这里处理切换准备状态...." + netData.msg);
        // }
        // else if(netData.protocolCode == ((int)NetApiType.JoinRoomResult)){
        //     Debug.Log("这里处理加入房间的结果..." + netData.msg);
        // }
        // else if(netData.protocolCode == ((int)NetApiType.GetRoomListResult)){
        //     
        // }
    }

    public void OnToCreateRoomButton(){
        LocalData.localData.createRoomPlane.SetActive(true);
    }
    public void OnExitSceneButton(){
        if(LocalData.localData.username == LocalData.localData.room.master){
            //TODO:Test 保存场景
            TestRecordScene.Test();
        }
        else if(LocalData.localData.isObserber){
            //TODO:Test 退出观战
            TestObserver.TestUnobserve();
        }
    }
    public class Userinfo{
        public string username{get;private set;}
        public string password{get;private set;}

        public Userinfo(string username, string password){
            this.username = username;
            this.password = password;
        }

    }

    public class RoomReadyInfo{
        public int status = 1;
        public RoomReadyInfo(int status){
            this.status = status;
        }
    }

}
