using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class SyncAttributeRequestMessage : Message
    {
        public string name;
        public Dictionary<string,string> att;
        public SyncAttributeRequestMessage(string name, Dictionary<string,string> att){
            this.name = name;
            this.att = att;
        }
        public override int GetMessageType()
        {
            return SyncAttributeRequestMessage;
        }
    }
}