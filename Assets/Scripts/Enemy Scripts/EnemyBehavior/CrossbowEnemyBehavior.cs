using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowEnemyBehavior : SimpleEnemyBehavior
{
    private Animator crossbowAnim;

    [SerializeField] private GameObject arrowPrefab;


    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attacking code
            //set animation
            //shoot arrow
            Rigidbody arr = Instantiate(arrowPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(90f, 0f, 0f)).GetComponent<Rigidbody>();
            arr.AddForce(transform.forward * 2f, ForceMode.Impulse);
            //do damage to player

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

}
