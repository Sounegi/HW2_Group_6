using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyBehavior : SimpleEnemyBehavior
{
    // Spearman run to player and attack so fast, but he need a cooldown
    //cooldown
    //public float cooldownTime;

    //animation
    //private Animator spearAnim;

    private bool triggered;

    private void Awake()
    {
        //particle = GameObject.Find("Blood Splat").GetComponent<ParticleSystem>();
        triggered = false;
        wait = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        //base.Awake();
        health = 1;
    }

    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attacking code
            //set animation
            //attack
            
            alreadyAttacked = true;
            wait = true;
            //cooldown
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    protected override void ResetAttack()
    {
        wait = false;
        alreadyAttacked = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("fastenemy_colided");
        
        if(other.tag == "Player" && !triggered)
        {
            triggered = true;
            Debug.Log("fast_enemy_collided");
            //Debug.Log("Hit da player");
            HealthManager.GetInstance().DoDamage(1);
            Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
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
