using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ClientHelper.Entities;
using UnityEngine;
using UnityEngine.UI;

public class LocalData : MonoBehaviour
{
    public static LocalData localData;
    private void Awake() {
        if(localData == null){
            localData = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    public string username;
    public string password;
    public Room room;
    public List<Room> roomList = new List<Room>();
    public Room[] roomArray;
    public List<Member> members = new List<Member>();
    public bool isReady;
    public bool isObserber;
    public Text usernameText;
    public Text passwordText;
    public GameObject menuPlane;
    public GameObject roomListPlane;
    public GameObject content;
    public GameObject roomPrefab;
    public Text newRoomName;
    public GameObject createRoomPlane;
    public GameObject roomPlane;
    public Text roomTitleText;
    public GameObject memberListContent;
    public GameObject memberPrefab;
    public GameObject syncPlane;
    public Transform syncItemsParent;

}
