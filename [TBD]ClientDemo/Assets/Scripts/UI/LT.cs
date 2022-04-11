using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LT : MonoBehaviour
{
    static Text logging;
    static int ind = 0;
    private void Awake() {
        logging = GetComponent<Text>();
    }

    public static void Logging(string s){
        if(logging == null) return;
        string colorCon = ind==0?"00FF00":"FFFFFF";
        logging.text += $"<color=#{colorCon}>{s}</color>\n";
        ind ^= 1;
        if(logging.text.Length > 1000){
            logging.text = "";
        }
    }
}
