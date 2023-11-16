using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public bool active;
    public event System.Action onChangeScene;

    void Start()
    {
        // this.enabled = false;   
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<CapsuleCollider>().enabled = false;
        
    }

    private void Update()
    {
        int enemy_remain = EnemyManager.GetInstance().CheckEnemyNum();
        //print(enemy_remain);
        if(enemy_remain <= 0)
        {
            enable_portal();
        }
    }

    public void enable_portal()
    {
        this.enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Indicator")// || obj.gameObject.layer == 3)
        {
            print("Trigger");
            MapManager.GetInstance().EndScene();
        }
    }
}
