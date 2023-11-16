using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthManager.GetInstance().DoDamage(1);
            Instantiate(particle, other.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.tag == "Ground") //|| other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
    }
}
