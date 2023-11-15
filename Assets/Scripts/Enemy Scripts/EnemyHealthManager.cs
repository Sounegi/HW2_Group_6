using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gothit;
    public event System.Action onHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        gothit = false;
        print("gak kepukul exit");
    }
    void OnTriggerEnter(Collider collide)
    {
        if(collide.tag == "Player")
        {
            print("kepukul");
            //this.enabled = false;
            onHit?.Invoke();
        }
        else
        {
            gothit = false;
            print("gak kepukul");
        }
        
    }
}
