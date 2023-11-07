using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private const float volume = 0.4f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthManager.GetInstance().AddHealth(1);
            AudioManager.GetInstance().PlaySoundEffect(1, volume);
            Destroy(gameObject);
        }
    }
}
