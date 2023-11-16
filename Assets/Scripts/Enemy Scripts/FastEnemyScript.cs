using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : EnemyTemplate
{

    bool wait;
    bool alreadyAttacked;
    float timeBetweenAttacks = 0;
    void Start() {
        max_hp = 1;
        hp = max_hp;
    }

    private void Update() {
        if (hp <= 0) {
            Die();
            return;
        }
        AttackPlayer(player);
    }

    protected void AttackPlayer(GameObject player)
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);

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

    protected void ResetAttack()
    {
        wait = false;
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Debug.Log("Hit da player ARUR");
            HealthManager.GetInstance().AddHealth(-1);
            Instantiate(playerBloodPrefab, collision.transform.Find("HeartPosition").position, Quaternion.identity);
            Die();
        }
    }
}
