using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestObserver : Test
{
    //Test:观战
    public static void TestObserve(string roomName){
        new NetSync().ObserveScene(roomName).Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "加入观战成功" : ("请求失败:" + reason) );
            if(msg.result){
                LocalData.localData.isObserber = true;
                //Debug.Log(LocalData.localData.room.ToText());
                TestEnterSence.EnterScene(LocalData.localData.room.sceneName);
                TestSceneSnapshot.Test();
            }
        });
    }

    //Test:取消观战
    public static void TestUnobserve(){
        new NetSync().UnobserveScene().Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo("请求" + (msg.result?"成功:":"失败:") + reason);
            if(msg.result){
                LocalData.localData.isObserber = false;
                TestRecordScene.EndScene();
            }
        });
    }

    private void Start() {
        //有用户加入观战
        NetAdapter.onSomeoneObserveScene += (username) => {
            InfomationPlane.ShowInfo($">用户[{username}]加入了观战");
        };
        //有用户退出观战
        NetAdapter.onSomeoneUnobserveScene += (username) => {
            InfomationPlane.ShowInfo($">用户[{username}]退出了观战");
        };
    }
}
