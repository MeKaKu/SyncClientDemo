using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

public class NetAdapter
{
    public static Action<Room> onSomeoneCreatedRoom;
    public static Action<string> onSomeoneEnteredRoom;
    public static Action<string> onSomeoneExitedRoom;
    public static Action<string> onEnterScene;
    public static Action<Item> onSomeoneCreatedItem;
    public static Action<string, Dictionary<string,string>> onSomeoneChangeAttribute;
    public static Action<string, bool> onSomeoneChangeReady;
    public static Action onSceneEnded;
    public static Action<string> onSomeoneOffline;
    public static Action<string> onSomeoneObserveScene;
    public static Action<string> onSomeoneUnobserveScene;
    public static Action<string> onSomeoneReconnect;
    public static void Adapter(NetData netData){
        int protocolCode = -netData.protocolCode;
        BroadcastMessage msg = JsonHelper.DeserializeObject<BroadcastMessage>(netData.msg);
        string dataJson = msg.data.ToString();
        if(protocolCode == Message.CreateRoomRequestMessage){
            //有人创建房间时触发
            Room room = JsonHelper.DeserializeObject<Room>(dataJson);
            onSomeoneCreatedRoom?.Invoke(room);
        }
        else if(protocolCode == Message.EnterRoomRequestMessage){
            //有用户加入房间时触发
            string someone = msg.data as string;
            onSomeoneEnteredRoom?.Invoke(someone);
        }
        else if(protocolCode == Message.SyncCreateRequestMessage){
            //有人创建物品时触发
            Item item = JsonHelper.DeserializeObject<Item>(dataJson);
            onSomeoneCreatedItem?.Invoke(item);
        }
        else if(protocolCode == Message.SyncAttributeRequestMessage){
            //有人改变物体属性
            ItemAtt itemAtt = JsonHelper.DeserializeObject<ItemAtt>(dataJson);
            onSomeoneChangeAttribute?.Invoke(itemAtt.name, itemAtt.att);
        }
        else if(protocolCode == Message.RoomReadyRequestMessage){
            //有人改变准备状态
            Member member = JsonHelper.DeserializeObject<Member>(dataJson);
            onSomeoneChangeReady?.Invoke(member.username, member.isReady);
        }
        else if(protocolCode == Message.ExitRoomRequestMessage){
            //有人退出房间时触发
            string username = msg.data as string;
            onSomeoneExitedRoom?.Invoke(username);
        }
        else if(protocolCode == Message.EnterSceneRequestMessage){
            //房间开始进入场景
            string sceneName = msg.data as string;
            onEnterScene?.Invoke(sceneName);
        }
        else if(protocolCode == Message.RecordSceneRequestMessage){
            //场景结束
            onSceneEnded?.Invoke();
        }
        else if(protocolCode == Message.OfflineMessage){
            //有人掉线了
            string username = msg.data as string;
            onSomeoneOffline?.Invoke(username);
        }
        else if(protocolCode == Message.ObserveRequestMessage){
            //有人加入观战
            string username = msg.data as string;
            onSomeoneObserveScene?.Invoke(username);
        }
        else if(protocolCode == Message.UnobserveRequestMessage){
            //有人退出观战
            string username = msg.data as string;
            onSomeoneUnobserveScene?.Invoke(username);
        }
        else if(protocolCode == Message.ReconnectRequestMessage){
            //有人重新连接
            string username = msg.data as string;
            onSomeoneReconnect?.Invoke(username);
        }
    }

    public class ItemAtt{
        public string name;
        public Dictionary<string,string> att;
    }
}
