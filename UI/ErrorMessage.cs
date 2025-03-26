using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class ErrorMessage : MonoBehaviour
{

    [SerializeField] GameObject errorCanvas;
    [SerializeField] GameObject genericErrorMessageObject;

    private bool errorMessageCooldown = false;

    IErrorService ErrorService = new ErrorService();

    private void Awake()
    {

        errorCanvas.SetActive(false);

    }

    public void ErrorMessageVoid(Exception e)
    {

        ErrorService.LogError(e);

        if (errorMessageCooldown == false)
        {

            errorMessageCooldown = true;
            StartCoroutine(ErrorMessageIE(e));

        }

    }

    IEnumerator ErrorMessageIE(Exception e)
    {

        GameObject saveManager = GameObject.FindGameObjectWithTag("SaveManager");
        TextMeshProUGUI genericErrorMessageText = genericErrorMessageObject.GetComponent<TextMeshProUGUI>();

        genericErrorMessageText.SetText("!FATAL ERROR!\n" + e.Message + "\n" + e.StackTrace + "\n!FATAL ERROR!");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            GameObject gameManager = GameObject.FindGameObjectWithTag("MenuManager");

            Destroy(gameManager);
            Destroy(saveManager);

        }
        else
        {

            GameObject gameManager = GameObject.FindGameObjectWithTag("UIManager");
            GameObject bean = GameObject.FindGameObjectWithTag("bean");
            GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            List<GameObject> enemies = new List<GameObject>();
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            PlayerCamScript playerCamScript = mainCam.GetComponent<PlayerCamScript>();

            Destroy(gameManager);
            Destroy(saveManager);
            Destroy(playerCamScript);
            Destroy(bean);

            foreach (GameObject enemy in enemies)
            {

                Destroy(enemy);

            }

        }

        errorCanvas.SetActive(true);
        Time.timeScale = 0;
        DOTween.KillAll();
        yield return new WaitForSecondsRealtime(40f);
        Application.Quit();

    }

}
