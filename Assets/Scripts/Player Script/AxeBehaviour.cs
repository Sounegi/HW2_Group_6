using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour
{
    private static AxeBehaviour instance;

    private SphereCollider col;
    private AudioManager audioPlayer;
    private bool attacking;

    void Awake()
    {
        instance = this;
        col = GetComponent<SphereCollider>();
        audioPlayer = GetComponent<AudioManager>();
    }

    public static AxeBehaviour GetInstance()
    {
        return instance;
    }

    void Start()
    {
        Reset();
    }

    public void Attack()
    {
        if(!attacking)
        {
            attacking = true;
            audioPlayer.PlayClip(0, 1.0f);
            col.enabled = true;
        }
    }

    public void Reset()
    {
        attacking = false;
        col.enabled = false;
    }

}
