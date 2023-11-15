using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerfound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //playerfound = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("inrange");
            playerfound = true;
        }else
            playerfound = false;
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("inrange");
            playerfound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("inrange");
            playerfound = false;
        }
    }
}
