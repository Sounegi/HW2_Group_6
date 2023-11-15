using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string PlayScene = "Main Menu";

    void Start()
    {
        ImageFade.GetInstance().SetColor(Color.black);
        ImageFade.GetInstance().StartFade(Color.clear, 1f, 1f);
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            StartCoroutine(ChangeScene(PlayScene));
        }
    }

    private IEnumerator ChangeScene(string scene)
    {
        ImageFade.GetInstance().StartFade(Color.black, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void OnValueChanged(float value)
    {
        DataManager.GetInstance().ChangeVolume(value);
    }
}
