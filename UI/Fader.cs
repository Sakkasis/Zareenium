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
    [SerializeField] GameObject fadeCanvas;
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
    public void FadeSetImageAlpha(float? alphaFromLocal, float? alphaToLocal, bool setCanvasActive)
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
        if (setCanvasActive == true)
        {

            fadeCanvas.SetActive(true);

        }

    }

    //if sceneInt is supplied load that scene
    public void FadeVoid(float? alphaFromLocal, float? alphaToLocal, float waitTime, int? scene)
    {

        DOTween.Kill(fadeTween);
        FadeSetImageAlpha(alphaFromLocal, alphaToLocal, true);

        StartCoroutine(FadeIE(waitTime, scene));

    }

    IEnumerator FadeIE(float waitTime, int? scene)
    {

        yield return new WaitForEndOfFrame();

        fadeTween = fadeImage.DOFade(alphaTo, waitTime)
                .SetUpdate(UpdateType.Normal, true)
                .SetAutoKill(true);

        fadeTween.Play();

        yield return new WaitForEndOfFrame();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(waitTime);

        if (scene.HasValue)
        {

            List<GameObject> canvasObjectList = new List<GameObject>();
            canvasObjectList.AddRange(GameObject.FindGameObjectsWithTag("Canvas"));

            foreach (GameObject canvasObject in canvasObjectList)
            {

                canvasObject.SetActive(false);

            }

            DOTween.KillAll();
            SceneManager.LoadScene(scene.Value);

        }
        else
        {

            fadeCanvas.SetActive(false);
            Time.timeScale = 1f;

        }

    }

    public void SetFadeCanvasActive(bool trueOnFalseOff)
    {

        if (trueOnFalseOff == true)
        {

            fadeCanvas.SetActive(true);

        }
        else if (trueOnFalseOff == false)
        {

            fadeCanvas.SetActive(false);

        }

    }

}
