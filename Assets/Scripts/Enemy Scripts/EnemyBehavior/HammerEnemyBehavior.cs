using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEnemyBehavior : SimpleEnemyBehavior
{
    private Animator hammerAnim;
    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attacking code
            //set animation
            //do damage to player

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
}
