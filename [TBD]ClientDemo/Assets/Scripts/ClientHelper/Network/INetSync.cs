using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public interface INetSync
    {
        Action<Action<ResponseMessage>> SyncCreateItem(string assetPath, string itemName);

        Action<Action<ResponseMessage>> SyncItemAttribute(string itemName, Dictionary<string,string> itemAttribute);

        Action<Action<ResponseMessage>> ItemAuthority(string itemName, bool isAuth);

        Action<Action<ResponseMessage>> GetSceneSnapshot();

        Action<Action<ResponseMessage>> RecordScene();

        Action<Action<ResponseMessage>> ObserveScene(string roomName);

        Action<Action<ResponseMessage>> UnobserveScene();

        Action<Action<ResponseMessage>> ReconnectScene();
    }
}