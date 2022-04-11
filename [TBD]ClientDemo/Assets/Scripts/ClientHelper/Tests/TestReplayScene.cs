using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestReplayScene : Test
{
    public static List<string> snapshots;
    public static int curInd;
    public static bool isReplaying;
    public static float frameTime = .1f;
    private static float nextFrameTime;
    public static void Test(){
        new NetRoom().ReplayScene(LocalData.localData.room.rid).Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "回放成功" : ("请求失败:" + reason) );
            if(msg.result){
                TestEnterSence.EnterScene(LocalData.localData.room.sceneName);
                snapshots = JsonHelper.DeserializeObject<List<string> >(msg.data.ToString());
                StartReplay();
            }
        });
    }

    private void Update() {
        ListenReplay();
    }

    public static void StartReplay(){
        isReplaying = true;
        curInd = 0;
        nextFrameTime = Time.time + frameTime;
    }

    public static void ListenReplay(){
        if(!isReplaying) return;
        if(curInd < snapshots.Count){
            if(Time.time > nextFrameTime){
                nextFrameTime = Time.time + frameTime;
                //TODO:next logic frame
                TestSceneSnapshot.SyncSnapshot(snapshots[curInd]);
                curInd ++;
            }
        }
        else{
            isReplaying = false;
            TestRecordScene.EndScene();
        }
    }
}
