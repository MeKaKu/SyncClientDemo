using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class RegisterRequestMessage : Message
    {
        public string username;
        public string password;

        public RegisterRequestMessage(string username, string password){
            this.username = username;
            this.password = password;
        }
        public override int GetMessageType()
        {
            return RegisterRequestMessage;
        }
    }
}