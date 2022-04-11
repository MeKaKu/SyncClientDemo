using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class CreateRoomRequestMessage : Message {
        public string name;
        public int capacity;
        public CreateRoomRequestMessage(string name, int capacity){
            this.name = name;
            this.capacity = capacity;
        }
        public override int GetMessageType()
        {
            return CreateRoomRequestMessage;
        }
    }

    public class GetRoomListRequestMessage : Message {
        public override int GetMessageType(){
            return GetRoomListRequestMessage;
        }
    }

    public class EnterRoomRequestMessage : Message
    {
        public string name;
        public EnterRoomRequestMessage(string name){
            this.name = name;
        }

        public override int GetMessageType()
        {
            return EnterRoomRequestMessage;
        }
    }

    public class ExitRoomRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return ExitRoomRequestMessage;
        }
    }

    public class RoomReadyRequestMessage : Message{
        public bool isReady;
        public RoomReadyRequestMessage(bool isReady){
            this.isReady = isReady;
        }
        public override int GetMessageType()
        {
            return RoomReadyRequestMessage;
        }
    }

}