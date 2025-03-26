using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{

    AudioManager audioManagerScript;

    [SerializeField] GameObject deathScreenCanvas;
    [SerializeField] TextMeshProUGUI deathReasonText;

    private void Start()
    {

        audioManagerScript = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }

    public void PlayerDiedVoid(float waitTime)
    {

        StartCoroutine(PlayerDiedIE(waitTime));

    }

    IEnumerator PlayerDiedIE(float waitTime)
    {

        audioManagerScript.TurnAudioOffOn(false, false, false, true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(waitTime);
        deathScreenCanvas.SetActive(true);
        yield return new WaitForEndOfFrame();
        deathReasonText.SetText("KRILL ISSUE");

    }

}
