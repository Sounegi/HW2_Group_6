using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private AudioManager audioMan;

    public float rotationSpeed = 30f;

    void Start()
    {
        audioMan = GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthManager.GetInstance().AddHealth(1);
            PotionSFXController.GetInstance().PlayClip();
            Destroy(gameObject);
        }
    }
}
