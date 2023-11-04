using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour {
    public HealthManager healthManager;
    private void Start() {
        healthManager = GameObject.Find("Hitpoints").GetComponent<HealthManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            healthManager.AddHealth(1);
            Destroy(gameObject);
        }
    }
}
