using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public class NetUser : INetUser
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="onResultAction">登录结果处理方法</param>
        public Action<Action<ResponseMessage>> Login(string username, string password)
        {
            return new NetAPI().Request(new LoginRequestMessage(username, password));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="usertype">用户类型</param>
        /// <param name="onResultAction">登录结果处理方法</param>
        public Action<Action<ResponseMessage>> Register(string username, string password)
        {
            return new NetAPI().Request(new RegisterRequestMessage(username, password));
        }
    }
}