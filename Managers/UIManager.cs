using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("Canvases")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject saveBeforeExitCanvas;

    [field: NonSerialized]
    public bool settingsOpen = false;
    [field: NonSerialized]
    public bool inventoryOpen = false;
    [field: NonSerialized]
    public bool saveBeforeExitOpen = false;

    PlayerCamScript camScript;

    private void Update()
    {

        if (camScript == null)
        {

            camScript = Camera.main.GetComponent<PlayerCamScript>();

        }

        if (Input.GetButtonDown("Settings") && inventoryOpen == false && saveBeforeExitOpen == false)
        {

            OpenSettingsVoid();

        }
        else if (Input.GetButtonDown("Settings") && settingsOpen == false && saveBeforeExitOpen == false)
        {

            OpenInventory();

        }
        else if (Input.GetButtonDown("Settings") && inventoryOpen == false && mainCanvas.activeSelf == false)
        {

            SaveBeforeExitVoid();

        }

    }

    public void OpenSettingsVoid()
    {

        settingsOpen = !settingsOpen;

        if (settingsOpen == true)
        {

            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            Time.timeScale = 0f;

            camScript.disableCam = true;

        }
        else
        {

            mainCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            Time.timeScale = 1f;

            camScript.disableCam = false;

        }

    }

    public void OpenInventory()
    {

        inventoryOpen = !inventoryOpen;

        if (inventoryOpen == true)
        {

            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            Time.timeScale = 0f;

            camScript.disableCam = true;

        }
        else
        {

            mainCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            Time.timeScale = 1f;

            camScript.disableCam = false;

        }

    }

    public void SaveBeforeExitVoid()
    {

        saveBeforeExitOpen = !saveBeforeExitOpen;

        if (saveBeforeExitOpen == true)
        {

            settingsOpen = false;
            settingsCanvas.SetActive(false);
            saveBeforeExitCanvas.SetActive(true);
            Time.timeScale = 0f;

            camScript.disableCam = true;

        }
        else
        {

            settingsOpen = true;
            settingsCanvas.SetActive(true);
            saveBeforeExitCanvas.SetActive(false);
            Time.timeScale = 0f;

            camScript.disableCam = true;

        }

    }

}
