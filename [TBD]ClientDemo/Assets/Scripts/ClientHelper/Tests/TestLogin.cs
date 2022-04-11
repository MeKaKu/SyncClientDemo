using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestLogin : Test
{
    /// <summary>
    /// 测试登录功能
    /// </summary>
    public static void Test(){
        new NetUser().Login(LocalData.localData.usernameText.text, LocalData.localData.passwordText.text).Invoke((msg) => {
            bool res = msg.result;
            if(res){
                InfomationPlane.ShowInfo("登陆成功");
                LocalData.localData.menuPlane.SetActive(false);
                LocalData.localData.roomListPlane.SetActive(true);
                LocalData.localData.username = LocalData.localData.usernameText.text;
                LocalData.localData.password = LocalData.localData.passwordText.text;
                //尝试重新连接
                TestReconnect.Test();
            }
            else{
                string reason = msg.data.ToString();
                InfomationPlane.ShowInfo("请求失败:" + reason);
            }
        });
    }
}
