using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Entities
{
    [System.Serializable]
    public class Room : object
    {
        public enum RoomState{
            None,
            Ready,
            Started,
            Paused,
            Scened,
            Overed,
            Recorded
        }
        public int rid;
        public string name;
        public string master;
        public int capacity;
        public int cnt;
        public string sceneName;
        public RoomState roomState;

        public string ToText()
        {
            return $"{name} ---用户{master}创建 | 人数:{cnt}/{capacity} | 状态:{roomState}";
        }
    }
}