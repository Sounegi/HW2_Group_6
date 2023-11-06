using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMaintainer : MonoBehaviour
{
    private static CanvasMaintainer instance;

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

    public static CanvasMaintainer GetInstance()
    {
        return instance;
    }
}
