using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private const float volume = 0.4f;
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthManager.GetInstance().AddHealth(1);
            // AudioManager.GetInstance().PlaySoundEffect(1, volume);
            Destroy(gameObject);
        }
    }
}
