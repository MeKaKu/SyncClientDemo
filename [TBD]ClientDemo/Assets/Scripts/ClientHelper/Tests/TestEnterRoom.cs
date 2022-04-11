using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestEnterRoom : Test
{
    /// <summary>
    /// 测试加入房间
    /// </summary>
    public static void Test(){
        new NetRoom().EnterRoom(LocalData.localData.room.name).Invoke((msg) => {
            if(msg.result){
                //Todo:
                List<Member> members = JsonHelper.DeserializeObjects<Member>(msg.data.ToString());
                LocalData.localData.members = members;
                //跳转房间界面
                LocalData.localData.createRoomPlane.SetActive(false);
                LocalData.localData.roomPlane.SetActive(true);
                LocalData.localData.roomTitleText.text = LocalData.localData.room.name;
                //更新成员列表
                foreach(var member in members){
                    GameObject memberBar = Instantiate<GameObject>(LocalData.localData.memberPrefab, LocalData.localData.memberListContent.transform);
                    memberBar.name = member.username;
                    memberBar.transform.Find("Name").GetComponent<Text>().text = member.username;
                    Text isReadyText = memberBar.transform.Find("IsReadyText").GetComponent<Text>();
                    isReadyText.text = member.isReady? "已准备" : "未准备";
                    isReadyText.color = member.isReady? Color.green : Color.red;
                }
            }
            InfomationPlane.ShowInfo(msg.result? "加入房间成功" : ("请求失败:" + msg.data.ToString()) );
        });
    }

    private void Start() {
        /// <summary>
        /// 处理有人加入房间时的广播消息
        /// </summary>
        NetAdapter.onSomeoneEnteredRoom += (username) => {
            InfomationPlane.ShowInfo($">用户[{username}]加入房间");
            if(username == LocalData.localData.username){
                return;
            }
            Member member = new Member(username, false);
            LocalData.localData.members.Add(member);
            GameObject memberBar = Instantiate<GameObject>(LocalData.localData.memberPrefab, LocalData.localData.memberListContent.transform);
            memberBar.name = member.username;
            memberBar.transform.Find("Name").GetComponent<Text>().text = member.username;
        };
    }
}
