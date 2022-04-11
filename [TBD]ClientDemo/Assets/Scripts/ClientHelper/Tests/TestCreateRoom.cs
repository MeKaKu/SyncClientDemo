using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestCreateRoom : Test
{
    /// <summary>
    /// 测试创建房间
    /// </summary>
    public static void Test(){
        new NetRoom().CreateRoom(LocalData.localData.newRoomName.text, 5).Invoke((msg) => {
            string info;
            if(msg.result){
                info = "创建房间成功";
                Room room = JsonHelper.DeserializeObject<Room>(msg.data.ToString());
                LocalData.localData.room = room;
                LocalData.localData.isReady = false;
                LocalData.localData.createRoomPlane.SetActive(false);
                LocalData.localData.roomPlane.SetActive(true);
                LocalData.localData.roomTitleText.text = room.name;
                LocalData.localData.members = new List<Member>();
                Member member = new Member(LocalData.localData.username, false);
                LocalData.localData.members.Add(member);

                GameObject memberBar = Instantiate<GameObject>(LocalData.localData.memberPrefab, LocalData.localData.memberListContent.transform);
                memberBar.name = member.username;
                memberBar.transform.Find("Name").GetComponent<Text>().text = member.username;
            }
            else{
                string reason = msg.data.ToString();
                info = "请求失败:" + reason;
            }
            InfomationPlane.ShowInfo(info);
        });
    }

    private void Start() {
        /// <summary>
        /// 处理当有玩家创建房间时的广播消息
        /// </summary>
        NetAdapter.onSomeoneCreatedRoom += (room) =>{
            InfomationPlane.ShowInfo($">用户[{room.master}]创建了新房间[{room.name}]");
            string objName = LocalData.localData.roomList.Count.ToString();
            LocalData.localData.roomList.Add(room);
            //TODO:处理新房间
            GameObject roomObject = Instantiate<GameObject>(LocalData.localData.roomPrefab, LocalData.localData.content.transform);
            roomObject.name = objName;
            roomObject.GetComponentInChildren<Text>().text = room.ToText();
        };
    }
}
