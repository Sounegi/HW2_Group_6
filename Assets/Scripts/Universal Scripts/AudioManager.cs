using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public List<AudioClip> clips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int index, float volume)
    {
        audioSource.clip = clips[0];
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.Play();
    }
}
