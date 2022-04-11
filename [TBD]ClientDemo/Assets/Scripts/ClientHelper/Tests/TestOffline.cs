using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOffline : Test
{
    private void Start() {
        NetAdapter.onSomeoneOffline += (username)=>{
            InfomationPlane.ShowInfo($">用户[{username}]掉线了");
        };
    }
}
