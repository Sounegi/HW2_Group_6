using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : EnemyTemplate
{
    void Start() {
        max_hp = 1;
        hp = max_hp;
    }

    private void Update() {
        if (hp <= 0) {
            Die();
            return;
        }
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player")) {
            HealthManager.GetInstance().AddHealth(-1);
            Instantiate(playerBloodPrefab, collision.transform.position, Quaternion.identity);
            Die();
        }
    }
}
