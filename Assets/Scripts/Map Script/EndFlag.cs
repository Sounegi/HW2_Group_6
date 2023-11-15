using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public bool active;
    public EnemyController tracker;

    void Start()
    {
        // this.enabled = false;   
        // GetComponent<MeshRenderer>().enabled = false;
        // GetComponent<CapsuleCollider>().enabled = false;
        // tracker = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        // tracker.OnEnd += enable_portal;
    }

    public void enable_portal()
    {
        this.enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
            MapManager.GetInstance().EndScene();
        }
    }
}
