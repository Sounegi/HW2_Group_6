using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            HealthManager.GetInstance().AddHealth(-1);
        }
    }

    public void ShootAt(GameObject player) {
        Vector3 direction = player.transform.position - transform.position;
        GetComponent<Rigidbody>().AddForce(direction.normalized * 1000, ForceMode.Force);
    }
}
