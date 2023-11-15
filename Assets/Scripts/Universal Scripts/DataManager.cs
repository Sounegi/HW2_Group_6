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
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static DataManager GetInstance()
    {
        return instance;
    }

    void OnValueChanged(float value)
    {
        volumeMultiplier = value;
    }
}
