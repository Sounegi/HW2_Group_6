using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    private static ImageFade instance;
    private Image image;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
    }

    public static ImageFade GetInstance()
    {
        return instance;
    }

    IEnumerator Fade(Color to, float duration, float delay, System.Action callback = null)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;
        Color from = image.color;
        
        float t = 0;

        while (t < 1)
        {
            image.color = Color.Lerp(from, to, t);
            yield return null;
            t = (Time.time - startTime) / duration;
        }

        yield return null;
    }

    public void StartFade(Color to, float duration, float delay = 0f, System.Action callback = null)
    {
        StartCoroutine(Fade(to, duration, delay));
    }
}
