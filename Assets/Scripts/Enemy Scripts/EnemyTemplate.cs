using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTemplate : MonoBehaviour {
    public int hp, max_hp;
    public NavMeshAgent agent;
    public GameObject player;
    public DetectAttack detect;
    public event System.Action onHit;

    void Awake () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        detect = GetComponentInChildren<DetectAttack>();
        onHit += AddHealthByNeg1;
    }

    // Update is called once per frame
    void Update() {
        if(hp <= 0) {
            Die();
        }
    }

    public void AddHealth(int deltaHealth) {
        hp += deltaHealth;
        if(hp > max_hp)
            hp = max_hp;
    }
    public void AddHealthByNeg1() {
        hp--;
    }

    public void Die() {
        EnemyManager.GetInstance().DecreaseEnemy();
        Destroy(gameObject);
    }

    public bool FoundPlayer() {
        return detect.playerfound;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            onHit?.Invoke();
        }
    }
}
