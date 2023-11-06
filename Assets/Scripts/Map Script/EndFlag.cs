using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public bool active;

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
            MapManager.GetInstance().EndScene();
        }
    }
}
