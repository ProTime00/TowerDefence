using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int sceneToFadeInto)
    {
        StartCoroutine(FadeOut(sceneToFadeInto));
    }

    private IEnumerator FadeIn()
    {
        var t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            var color = img.color;
            var a = curve.Evaluate(t);
            color.a = a;
            img.color = color;
            yield return 0;
        }
    }
    
    private IEnumerator FadeOut(int scene)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            var color = img.color;
            var a = curve.Evaluate(t);
            color.a = a;
            img.color = color;
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
