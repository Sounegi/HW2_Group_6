using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //do damage to player
            HealthManager.GetInstance().DoDamage(1);
        }
        Destroy(this.gameObject);
    }
}
