using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Messages;
using Assets.Scripts.ClientHelper.Network;
using UnityEngine;

public class TestItemAuthority : Test
{
    public static Action<Action<ResponseMessage>> Test(string itemName, bool isAuth){
        return new NetSync().ItemAuthority(itemName, isAuth);
    }

}
