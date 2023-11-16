using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehavior : SimpleEnemyBehavior
{
    //private Animator crossbowAnim;

    [SerializeField] private GameObject bulletPrefab;
    private bool triggered;
    private void Awake()
    {
        alreadyAttacked = false;
        triggered = false;
        wait = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        //base.Awake();
        health = 1;
        timeBetweenAttacks = 3f;
    }

    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        if (!alreadyAttacked)
        {
            //Attacking code
            //set animation
            //shoot arrow
            Rigidbody arr = Instantiate(bulletPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            arr.AddForce(transform.forward * 10f, ForceMode.Impulse);
            //do damage to player

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fastenemy_colided");

        if (other.tag == "Player" && !triggered)
        {
            triggered = true;
            Debug.Log("Hit da player");
            HealthManager.GetInstance().DoDamage(1);
        }

        if (other.tag == "Axe")
        {
            Debug.Log("Hit monster");
            TakeDamage(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        //this.GetComponent<BoxCollider>().enabled = true;
    }

}
