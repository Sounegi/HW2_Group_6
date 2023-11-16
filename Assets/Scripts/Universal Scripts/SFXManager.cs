using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip() {
        audioSource.clip = clip;
        audioSource.volume = Mathf.Clamp01(0.4f * DataManager.GetInstance().GetVolume());
        audioSource.Play();
    }
}