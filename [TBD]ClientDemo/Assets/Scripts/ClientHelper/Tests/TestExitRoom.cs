using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestExitRoom : Test
{
    public static void Test(){
        new NetRoom().ExitRoom().Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "离开房间成功" : ("请求失败:" + reason) );
            if(msg.result){
                Transform trans = LocalData.localData.memberListContent.transform.Find(LocalData.localData.username);
                Destroy(trans.gameObject);
                ClearMemberList();
            }
        });
    }

    private void Start() {
        NetAdapter.onSomeoneExitedRoom += (string username)=>{
            InfomationPlane.ShowInfo($">用户[{username}]退出了房间");
            //清除退出房间的用户
            Transform trans = LocalData.localData.memberListContent.transform.Find(username);
            Destroy(trans.gameObject);
            LocalData.localData.room.cnt -- ;
            //房主退出
            if(username == LocalData.localData.room.master){
                InfomationPlane.ShowInfo("房间被解散了");
                ClearMemberList();
            }
        };
    }

    public static void ClearMemberList(){
        //清空房间成员列表
        for(int i = LocalData.localData.memberListContent.transform.childCount - 1; i>=0; i--){
            Destroy(LocalData.localData.memberListContent.transform.GetChild(i).gameObject);
        }
        LocalData.localData.roomPlane.SetActive(false);
        LocalData.localData.isReady = false;
    }
}
