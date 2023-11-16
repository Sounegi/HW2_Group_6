using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehavior : SimpleEnemyBehavior
{
    //private Animator crossbowAnim;

    [SerializeField] private GameObject bulletPrefab;
    private void Awake()
    {
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
        if (other.tag == "Axe")
        {
            Debug.Log("Range kena Hit Player");
            TakeDamage(1);
        }
    }

}
