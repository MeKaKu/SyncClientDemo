using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAbleUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool isAuth;
    Vector3 preMousePosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //对物体的操作权限？
        preMousePosition = Input.mousePosition;
        TestItemAuthority.Test(name, true).Invoke((msg) => {
            string reason = msg.data.ToString();
            Debug.Log("请求" + (msg.result?"成功:":"失败:") + reason);
            if(msg.result){
                isAuth = true;
            }
        });
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isAuth)
        TestSyncAttribute.Test(transform.name,
                                ToAttribute(transform.localPosition + (Input.mousePosition - preMousePosition).normalized*10));
        preMousePosition = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        //取消对物体的操作权限
        TestItemAuthority.Test(name, false).Invoke((msg) => {
            string reason = msg.data.ToString();
            Debug.Log("请求" + (msg.result?"成功:":"失败:") + reason);
            if(msg.result){
                isAuth = false;
            }
        });
    }

    public static Dictionary<string,string> ToAttribute(Vector3 pos){
        Dictionary<string,string> att = new Dictionary<string, string>();
        att.Add(AttID.POSITION, Vector3ToString(pos));
        return att;
    }
    public void ToTransValue(Dictionary<string,string> att){
        if(att.ContainsKey(AttID.POSITION)){
            transform.localPosition = StringToVector3(att[AttID.POSITION]);
        }
    }

    public static string Vector3ToString(Vector3 vec){
        return $"{vec.x},{vec.y},{vec.z}";
    }
    public static Vector3 StringToVector3(string str){
        string[] ss = str.Split(',');
        return new Vector3(float.Parse(ss[0]), float.Parse(ss[1]), float.Parse(ss[2]));
    }
}

public class AttID{
    public const string POSITION = "pos";
    public const string ROTATION = "rot";
    public const string SCALE = "scale";
}
