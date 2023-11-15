using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyScript : EnemyTemplate
{
    // Start is called before the first frame update
    void Start() {
        max_hp = 3;
        hp = max_hp;
    }

    // Update is called once per frame
    void Update() {
        if (hp <= 0) {
            Die();
            return;
        }
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            HealthManager.GetInstance().AddHealth(-2);
        }
    }
}
