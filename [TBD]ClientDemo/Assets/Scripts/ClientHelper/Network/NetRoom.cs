using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public class NetRoom : INetRoom
    {
        public Action<Action<ResponseMessage>> CreateRoom(string roomName, int capacity)
        {
            return new NetAPI().Request(new CreateRoomRequestMessage(roomName, capacity));
        }

        public Action<Action<ResponseMessage>> EnterRoom(string roomName)
        {
            return new NetAPI().Request(new EnterRoomRequestMessage(roomName));
        }

        public Action<Action<ResponseMessage>> EnterScene(string sceneName)
        {
            return new NetAPI().Request(new EnterSceneRequestMessage(sceneName));
        }

        public Action<Action<ResponseMessage>> ExitRoom()
        {
            return new NetAPI().Request(new ExitRoomRequestMessage());
        }

        public Action<Action<ResponseMessage>> GetRecordedRoomList()
        {
            return new NetAPI().Request(new RecordedSceneRequestMessage());
        }

        public Action<Action<ResponseMessage>> GetRoomList()
        {
            return new NetAPI().Request(new GetRoomListRequestMessage());
        }

        public Action<Action<ResponseMessage>> ReplayScene(int rid)
        {
            return new NetAPI().Request(new AllSnapshotRequestMessage(rid));
        }

        public Action<Action<ResponseMessage>> RoomReady(bool isReady)
        {
            return new NetAPI().Request(new RoomReadyRequestMessage(isReady));
        }
    }
}