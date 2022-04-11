using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestReady : Test
{
    public static void Test(){
        new NetRoom().RoomReady(!LocalData.localData.isReady).Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? (LocalData.localData.isReady? "准备" : "取消准备") + "成功" : ("请求失败:" + reason) );
            if(msg.result){
                LocalData.localData.isReady = !LocalData.localData.isReady;
                UpdateReady(LocalData.localData.username, LocalData.localData.isReady);
            }
        });
    }

    private void Start() {
        NetAdapter.onSomeoneChangeReady += (string username, bool isReady)=>{
            InfomationPlane.ShowInfo($">用户[{username}]" + (isReady?"准备":"取消准备") );
            foreach(var member in LocalData.localData.members){
                if(member.username == username){
                    member.isReady = isReady;
                    UpdateReady(username, isReady);
                }
            }
        };
    }

    static void UpdateReady(string username, bool isReady){
        Transform memberTrans = LocalData.localData.memberListContent.transform.Find(username);
        Text isReadyText = memberTrans.Find("IsReadyText").GetComponent<Text>();
        isReadyText.text = isReady? "已准备" : "未准备";
        isReadyText.color = isReady? Color.green : Color.red;
    }
}
