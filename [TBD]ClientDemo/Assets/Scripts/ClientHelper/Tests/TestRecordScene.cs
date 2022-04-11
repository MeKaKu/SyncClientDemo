using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestRecordScene : Test
{
    public static void Test(){
        new NetSync().RecordScene().Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "保存场景成功" : ("请求失败:" + reason) );
            if(msg.result){
                EndScene();
            }
        });
    }

    private void Start() {
        NetAdapter.onSceneEnded += ()=>{
            InfomationPlane.ShowInfo("场景已结束");
            EndScene();
        };
    }

    public static void EndScene(){
        LocalData.localData.syncPlane.SetActive(false);
        TestExitRoom.ClearMemberList();
        for(int i = LocalData.localData.syncItemsParent.childCount - 1; i>=0; i--){
            Destroy(LocalData.localData.syncItemsParent.GetChild(i).gameObject);
        }
    }
}
