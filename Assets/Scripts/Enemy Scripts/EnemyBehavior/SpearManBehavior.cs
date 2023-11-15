using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearManBehavior : SimpleEnemyBehavior
{
    // Spearman run to player and attack so fast, but he need a cooldown
    //cooldown
    public float cooldownTime;

    //animation
    private Animator spearAnim;

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
            //cooldown
            Invoke(nameof(CoolDown), timeBetweenAttacks);
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void CoolDown()
    {
        agent.SetDestination(transform.position);
        //yield WaitForSeconds(cooldownTime);

        Invoke(nameof(ResetAttack), 0.5f);
    }
}
