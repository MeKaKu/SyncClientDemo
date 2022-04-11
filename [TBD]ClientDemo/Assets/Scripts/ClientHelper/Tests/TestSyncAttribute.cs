using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestSyncAttribute : Test
{
    public static void Test(string itemName, Dictionary<string,string> itemAttribute){
        new NetSync().SyncItemAttribute(itemName, itemAttribute).Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "改变物体属性成功" : ("请求失败:" + reason) );
            if(msg.result){
                UpdateItemAttribute(itemName, itemAttribute);
            }
        });
    }

    private void Start() {
        NetAdapter.onSomeoneChangeAttribute += (itemName, itemAttribute)=>{
            InfomationPlane.ShowInfo($">物体[{itemName}]属性改变为[{itemAttribute}]");
            UpdateItemAttribute(itemName, itemAttribute);
        };
    }

    static void UpdateItemAttribute(string itemName, Dictionary<string,string> itemAttribute){
        Transform trans = LocalData.localData.syncItemsParent.Find(itemName);
        DragAbleUI dragAbleUI = trans.GetComponent<DragAbleUI>();
        dragAbleUI.ToTransValue(itemAttribute);
    }
}
