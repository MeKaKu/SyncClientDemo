using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestSyncCreate : Test
{
    /// <summary>
    /// 测试同步创建物体
    /// </summary>
    public static void Test(){
        new NetSync().SyncCreateItem("Img_Cube","Img_Cube").Invoke((msg) => {
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "创建物体成功" : ("请求失败:" + reason) );
            if(msg.result){
                CreateItem("Img_Cube", "Img_Cube");
            }
        });
    }

    public static void Test02(){
        new NetSync().SyncCreateItem("Img_Circle","Img_Circle").Invoke((msg) => {
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo("请求" + (msg.result?"成功:":"失败:") + reason);
            if(msg.result){
                CreateItem("Img_Circle", "Img_Circle");
            }
        });
    }

    private void Start() {
        NetAdapter.onSomeoneCreatedItem += (item)=>{
            InfomationPlane.ShowInfo($">物体[{item.name}]被创建");
            string assetPath = item.asset;
            string itemName = item.name;
            //客户端创建物体
            CreateItem(assetPath, itemName);
        };
    }

    public static void CreateItem(string assetPath, string itemName){
        GameObject itemPrefab = Resources.Load<GameObject>(assetPath);
        GameObject newItem = Instantiate<GameObject>(itemPrefab, LocalData.localData.syncItemsParent);
        newItem.name = itemName;
    }
}
