using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    public List<string> nextScene;
    private int sceneIDX = 0;

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

    public static MapManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        Reset();
    }

    public void EndScene()
    {
        StartCoroutine(ChangeScene(nextScene[sceneIDX]));
    }

    private IEnumerator ChangeScene(string scene)
    {
        ImageFade.GetInstance().StartFade(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
        if(nextScene[sceneIDX] == "Main Menu")
        {
            Destroy(gameObject);
            Destroy(MapManager.GetInstance().gameObject);
        }
        else
        {
            sceneIDX += 1;
            Reset();
        }   
    }

    private void Reset()
    {
        ImageFade.GetInstance().SetColor(Color.black);
        ImageFade.GetInstance().StartFade(Color.clear, 1f, 1f);
    }
}
