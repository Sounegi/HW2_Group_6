using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> SFXs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySoundEffect(int index) {
        audioSource.PlayOneShot(SFXs[index]);
    }
}
