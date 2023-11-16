using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    [Header("Data")]
    [SerializeField] float volumeMultiplier = 1.0f;

    void Awake()
    {
        if(instance == null)
		{	
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static DataManager GetInstance()
    {
        return instance;
    }

    public void ChangeVolume(float value)
    {
        volumeMultiplier = value;
    }

    public float GetVolume()
    {
        return volumeMultiplier;
    }
}
