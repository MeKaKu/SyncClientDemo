using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceClass{
    public event Action onAction;
    public static InstanceClass instance = new InstanceClass();

    public void OnAction(){
        Debug.Log("OnAction.....");
        onAction?.Invoke();
    }
}

public class AABB{
    public event Action onResult;

    public void hhh(){
        InstanceClass.instance.onAction -= OnResult;
    }
    public void Request(Action fun){ 
        onResult = fun;
        InstanceClass.instance.onAction -= OnResult;
        InstanceClass.instance.onAction += OnResult;
    }

    public void OnResult(){
        Debug.Log("OnResult....");
        onResult?.Invoke();
        InstanceClass.instance.onAction -= OnResult;
    }
}

public class TestAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AABB a = new AABB();
        a.hhh();
        a.Request(()=>{
            Debug.Log("a 01 ....");
        });
        a.Request(()=>{
            Debug.Log("a 02 ....");
        });
        AABB b = new AABB();

        InstanceClass.instance.OnAction();

        b.Request(()=>{
            Debug.Log("b 01 ....");
        });
        b.Request(()=>{
            Debug.Log("b 02 ....");
        });

        InstanceClass.instance.OnAction();

        TestFunc();

        InstanceClass.instance.OnAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestFunc(){
        AABB c = new AABB();
        c.Request(()=>{
            Debug.Log("c 01 ....");
        });
        c.Request(()=>{
            Debug.Log("c 02 ....");
        });

        InstanceClass.instance.OnAction();
    }
}
