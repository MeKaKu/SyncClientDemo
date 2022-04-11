using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfomationPlane : MonoBehaviour
{
    private static Text infoText;
    private void Start() {
        if(infoText == null){
            infoText = transform.Find("InfoText").GetComponent<Text>();
        }
    }
    public static void ShowInfo(string info){
        infoText.text = info;
    }
}
