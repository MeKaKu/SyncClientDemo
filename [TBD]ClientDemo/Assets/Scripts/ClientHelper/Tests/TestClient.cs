using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class TestClient : MonoBehaviour
{
    string ip = "127.0.0.1";
    //string ip = "10.16.80.59";
    int port = 8080;//10110

    AsyncClient asyncClient;
    async void Start()
    {
        asyncClient = new AsyncClient();

        asyncClient.Connect(ip, port);

        string s = JsonConvert.SerializeObject(new Userinfo("name", "pwd"));
        
        Debug.Log(s);

        Userinfo testData = JsonConvert.DeserializeObject<Userinfo>(s);

        Debug.Log($"{testData.username}, {testData.password}");

        await Task.Delay(1000);

        Login(testData);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Userinfo{
        public string username{get;private set;}
        public string password{get;private set;}

        public Userinfo(string username, string password){
            this.username = username;
            this.password = password;
        }

    }

    public class ResultState{
        public int code;
        public int length;
        public object data;
    }

    public bool Login(Userinfo userinfo){

        asyncClient.Send(JsonConvert.SerializeObject(userinfo));

        return false;
    }

}
