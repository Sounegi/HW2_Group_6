using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> SFXs;

    public static AudioManager instance;
    public float sfxVolume = 1.0f;

    private AudioSource audioSource;
    private void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public static AudioManager GetInstance() {
        return instance;
    }

    public void PlaySoundEffect(int index, float volume) {
        audioSource.PlayOneShot(SFXs[index], volume * sfxVolume);
    }
}
