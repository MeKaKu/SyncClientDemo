using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Messages
{
    public class EnterSceneRequestMessage : Message
    {
        public string sceneName;
        public EnterSceneRequestMessage(string sceneName){
            this.sceneName = sceneName;
        }
        public override int GetMessageType()
        {
            return EnterSceneRequestMessage;
        }
    }

    public class SceneSnapshotRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return SceneSnapshotRequestMessage;
        }
    }

    public class RecordSceneRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return RecordSceneRequestMessage;
        }
    }

    public class RecordedSceneRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return RecordedSceneRequestMessage;
        }
    }

    public class ObserveRequestMessage : Message
    {
        public string name;
        public ObserveRequestMessage(string roomName){
            this.name = roomName;
        }
        public override int GetMessageType()
        {
            return ObserveRequestMessage;
        }
    }

    public class UnobserveRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return UnobserveRequestMessage;
        }
    }

    public class AllSnapshotRequestMessage : Message
    {
        public int rid;
        public AllSnapshotRequestMessage(int rid){
            this.rid = rid;
        }
        public override int GetMessageType()
        {
            return AllSnapshotRequestMessage;
        }
    }
}