using System;
using Assets.Scripts.ClientHelper.Messages;

namespace Assets.Scripts.ClientHelper.Network
{
    public interface INetUser
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>结果处理委托</returns>
        public Action<Action<ResponseMessage>> Login(string username, string password);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// 
        public Action<Action<ResponseMessage>> Register(string username, string password);
    }
}