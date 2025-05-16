using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Fader : MonoBehaviour
{

    [SerializeField] Image fadeImage;
    Tweener fadeTween;

    float alphaFrom;
    float alphaTo;

    private void Awake()
    {

        DOTween.Init(false, false, LogBehaviour.Default).SetCapacity(200, 10);
        DOTween.defaultTimeScaleIndependent = true;
        DOTween.defaultUpdateType = UpdateType.Normal;
        DOTween.defaultAutoKill = false;
        DOTween.defaultAutoPlay = AutoPlay.None;

    }

#nullable enable
    public void FadeSetImageAlpha(float? alphaFromLocal, float? alphaToLocal)
    {

        if (alphaFromLocal.HasValue)
        {

            alphaFrom = alphaFromLocal.Value;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alphaFrom);

        }
        if (alphaToLocal.HasValue)
        {

            alphaTo = alphaToLocal.Value;

        }

    }

    //if sceneInt is supplied load that scene
    public void FadeVoid(float? alphaFromLocal, float? alphaToLocal, float waitTime, int? scene)
    {

        DOTween.Kill(fadeTween);
        FadeSetImageAlpha(alphaFromLocal, alphaToLocal);

        StartCoroutine(FadeIE(waitTime, scene));

    }

    IEnumerator FadeIE(float waitTime, int? scene)
    {

        CanvasManager canvasScript = gameObject.GetComponent<CanvasManager>();
        canvasScript.Fade(0);

        yield return new WaitForEndOfFrame();

        fadeTween = fadeImage.DOFade(alphaTo, waitTime)
                .SetUpdate(UpdateType.Normal, true)
                .SetAutoKill(true);

        fadeTween.Play();

        yield return new WaitForEndOfFrame();
        canvasScript.Fade(1);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(waitTime);

        if (scene.HasValue)
        {

            DOTween.KillAll();
            SceneManager.LoadScene(scene.Value);

        }
        else
        {

            canvasScript.Fade(2);
            Time.timeScale = 1f;

        }

    }
#nullable disable

}
