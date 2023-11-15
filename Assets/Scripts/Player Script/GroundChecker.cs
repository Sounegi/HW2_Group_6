using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Ground")
        {
            PlayerController.GetInstance().ChangeGroundState(true);
        }
    }
}
