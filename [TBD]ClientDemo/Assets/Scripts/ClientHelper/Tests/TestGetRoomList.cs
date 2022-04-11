using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestGetRoomList : Test
{
    /// <summary>
    /// 测试获取房间列表
    /// </summary>
    public static void Test(){
        new NetRoom().GetRoomList().Invoke((msg)=>{
            if(msg.result){
                List<Room> rooms = new List<Room>();
                rooms = JsonHelper.DeserializeObjects<Room>(msg.data.ToString());
                ShowRoomList(rooms);
            }
            string reason = msg.data.ToString();
            InfomationPlane.ShowInfo(msg.result? "获取房间列表成功" : ("请求失败:" + reason) );
        });
    } 

    public static void ShowRoomList(List<Room> rooms){
        for(int i = LocalData.localData.content.transform.childCount - 1; i >= 0 ; i--){
            Destroy(LocalData.localData.content.transform.GetChild(i).gameObject);
        }
        LocalData.localData.roomList = new List<Room>(rooms);
        int roomCnt = rooms.Count;
        for(int i = 0; i < roomCnt; i++){
            GameObject roomObject = Instantiate<GameObject>(LocalData.localData.roomPrefab, LocalData.localData.content.transform);
            roomObject.name = i.ToString();
            roomObject.GetComponentInChildren<Text>().text = rooms[i].ToText();
        }
    }
}
