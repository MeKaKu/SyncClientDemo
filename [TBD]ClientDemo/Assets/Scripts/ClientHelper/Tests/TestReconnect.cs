using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestReconnect : Test
{
    public static void Test(){
        new NetSync().ReconnectScene().Invoke((msg)=>{
            string reason = msg.data.ToString();
            if(msg.result){
                InfomationPlane.ShowInfo("重新连接成功");
                Room room = JsonHelper.DeserializeObject<Room>(msg.data.ToString());
                TestEnterSence.EnterScene(room.sceneName);
                TestSceneSnapshot.Test();
            }
        });
    }

    private void Start() {
        NetAdapter.onSomeoneReconnect += (username)=>{
            InfomationPlane.ShowInfo($">用户[{username}]重新连接");
        };
    }
}
