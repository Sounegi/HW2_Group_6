using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;

    public string nextScene;

    public string gameOver;

    void Awake()
    {   
        if(instance == null)
		{	
			instance = this;
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
        HealthManager.GetInstance().Reset();
    }

    public void EndScene()
    {
        StartCoroutine(ChangeScene(nextScene));
    }

    public void GameOver()
    {
        StartCoroutine(ChangeScene(gameOver));
    }

    private IEnumerator ChangeScene(string scene)
    {
        ImageFade.GetInstance().StartFade(Color.black, 2f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene);
    }

    private void Reset()
    {
        ImageFade.GetInstance().SetColor(Color.black);
        ImageFade.GetInstance().StartFade(Color.clear, 1f, 1f);
    }
}
