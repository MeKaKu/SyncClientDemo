using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Assets.Scripts.ClientHelper.Entities.Room;

public class RoomButton : Test, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        int index = int.Parse(gameObject.name);
        LocalData.localData.room = LocalData.localData.roomList[index];
        if(LocalData.localData.room == null){
            Debug.Log("房间不存在！");
        }
        if(LocalData.localData.room.roomState == RoomState.Ready){
            TestEnterRoom.Test();
        }
        else if(LocalData.localData.room.roomState == RoomState.Scened){
            //TODO:Observe
            TestObserver.TestObserve(LocalData.localData.room.name);
        }
        else if(LocalData.localData.room.roomState == RoomState.Overed){
            //TestReplayScene();
            TestReplayScene.Test();
        }
    }
}
