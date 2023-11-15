using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyBehavior : SimpleEnemyBehavior
{
    //private Animator hammerAnim;
    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attacking code
            //set animation
            Invoke(nameof(AttackMotion), 0.5f);
            //do damage to player

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void AttackMotion()
    {
        
    }
}
