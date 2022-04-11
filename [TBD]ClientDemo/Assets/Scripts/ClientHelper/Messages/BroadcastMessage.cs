using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class BroadcastMessage : Message
    {
        public object data;
        public override int GetMessageType()
        {
            return 0;
        }
    }
}