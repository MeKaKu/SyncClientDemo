using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestSceneSnapshot : Test
{
    public static void Test(){
        new NetSync().GetSceneSnapshot().Invoke((msg)=>{
            if(!msg.result) return;
            Debug.Log("场景快照:" + msg.data);

            //TODO:场景同步
            SyncSnapshot(msg.data as string);
            
        });
    }

    public static void SyncSnapshot(string _snapshot){
        Dictionary<string, Dictionary<string, string>> snapshot 
            = JsonHelper.DeserializeObject< Dictionary<string, Dictionary<string, string>> >(_snapshot);
        foreach(var item in snapshot){
            string itemName = item.Key;
            Dictionary<string,string> attDict = item.Value;
            Transform trans = LocalData.localData.syncItemsParent.Find(itemName);
            //物体不存在，进行创建
            if(null == trans){
                TestSyncCreate.CreateItem(attDict["asset"], itemName);
                trans = LocalData.localData.syncItemsParent.Find(itemName);
            }
            //给物体属性赋值
            DragAbleUI dragAbleUI = trans.GetComponent<DragAbleUI>();
            dragAbleUI.ToTransValue(attDict);
        }
    }
}
