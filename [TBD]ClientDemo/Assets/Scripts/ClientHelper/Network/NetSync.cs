using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public class NetSync : INetSync
    {
        public Action<Action<ResponseMessage>> GetSceneSnapshot()
        {
            return new NetAPI().Request(new SceneSnapshotRequestMessage());
        }

        public Action<Action<ResponseMessage>> ItemAuthority(string itemName, bool isAuth)
        {
            return new NetAPI().Request(new ItemAuthorityRequestMessage(itemName, isAuth));
        }

        public Action<Action<ResponseMessage>> ObserveScene(string roomName)
        {
            return new NetAPI().Request(new ObserveRequestMessage(roomName));
        }

        public Action<Action<ResponseMessage>> ReconnectScene()
        {
            return new NetAPI().Request(new ReconnectRequestMessage());
        }

        public Action<Action<ResponseMessage>> RecordScene()
        {
            return new NetAPI().Request(new RecordSceneRequestMessage());
        }

        public Action<Action<ResponseMessage>> SyncCreateItem(string assetPath, string itemName)
        {
            return new NetAPI().Request(new SyncCreateRequestMessage(assetPath, itemName));
        }

        public Action<Action<ResponseMessage>> SyncItemAttribute(string itemName, Dictionary<string,string> itemAttribute)
        {
            return new NetAPI().Request(new SyncAttributeRequestMessage(itemName, itemAttribute));
        }

        public Action<Action<ResponseMessage>> UnobserveScene()
        {
            return new NetAPI().Request(new UnobserveRequestMessage());
        }
    }
}