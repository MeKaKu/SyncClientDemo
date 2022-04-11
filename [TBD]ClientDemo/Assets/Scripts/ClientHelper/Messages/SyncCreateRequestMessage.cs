using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class SyncCreateRequestMessage : Message
    {
        public string asset;
        public string name;
        public SyncCreateRequestMessage(string assetPath, string itemName){
            asset = assetPath;
            name = itemName;
        }
        public override int GetMessageType()
        {
            return SyncCreateRequestMessage;
        }
    }

    public class ItemAuthorityRequestMessage : Message
    {
        public string name;
        public bool isAuth;
        public ItemAuthorityRequestMessage(string name, bool isAuth){
            this.name = name;
            this.isAuth = isAuth;
        }
        public override int GetMessageType()
        {
            return ItemAuthorityRequestMessage;
        }
    }
}