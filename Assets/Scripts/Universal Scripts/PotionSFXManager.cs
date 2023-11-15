using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSFXController : MonoBehaviour
{
    private static PotionSFXController instance;

    private AudioSource audioSource;

    public AudioClip clip;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public static PotionSFXController GetInstance()
    {
        return instance;
    }

    public void PlayClip()
    {
        audioSource.clip = clip;
        audioSource.volume = Mathf.Clamp01(0.4f * DataManager.GetInstance().GetVolume());
        audioSource.Play();
    }
}
