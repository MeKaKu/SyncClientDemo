using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public interface INetRoom
    {
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns>结果处理委托</returns>
        Action<Action<ResponseMessage>> GetRoomList();

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="roomName">房间名</param>
        /// <returns>结果处理委托</returns>
        Action<Action<ResponseMessage>> CreateRoom(string roomName, int capacity);

        /// <summary>
        /// 准备
        /// </summary>
        /// <param name="isReady">是否准备</param>
        /// <returns>结果处理委托</returns>
        Action<Action<ResponseMessage>> RoomReady(bool isReady);

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="roomName">房间名</param>
        /// <returns>结果处理委托</returns>
        Action<Action<ResponseMessage>> EnterRoom(string roomName);

        /// <summary>
        /// 退出房间
        /// </summary>
        /// <returns>结果处理委托</returns>
        Action<Action<ResponseMessage>> ExitRoom();

        Action<Action<ResponseMessage>> EnterScene(string sceneName);

        Action<Action<ResponseMessage>> GetRecordedRoomList();

        Action<Action<ResponseMessage>> ReplayScene(int rid);
    }
}