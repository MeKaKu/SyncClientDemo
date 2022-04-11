using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestGetRecordedScene : Test
{
    public static void Test(){
        new NetRoom().GetRecordedRoomList().Invoke((msg)=>{
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "获取保存的房间列表成功" : ("请求失败:" + reason) );
            if(msg.result){
                List<Room> rooms = JsonHelper.DeserializeObjects<Room>(msg.data.ToString());
                TestGetRoomList.ShowRoomList(rooms);
            }
        });
    }
}
