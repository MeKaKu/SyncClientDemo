using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestEnterSence : Test
{

    public static void Test(){
        string sceneName = "SyncScene";
        new NetRoom().EnterScene(sceneName).Invoke( (msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "加入场景成功" : ("请求失败:" + reason) );
            if(msg.result){
                EnterScene(sceneName);
            }
        } );
    }

    private void Start() {
        NetAdapter.onEnterScene += (sceneName)=>{
            InfomationPlane.ShowInfo($">进入场景[{sceneName}]");
            EnterScene(sceneName);
        };
    }

    public static void EnterScene(string sceneName){
        LocalData.localData.roomPlane.SetActive(false);
        LocalData.localData.syncPlane.SetActive(true);
        // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        // asyncOperation.completed += (s)=>{
        //     Debug.Log("异步加载场景完成了");
        // };
    }
}
