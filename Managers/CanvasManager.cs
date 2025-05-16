using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] GameObject fadeCanvas;            //0
    [SerializeField] GameObject errorCanvas;           //1
    [SerializeField] GameObject mainCanvas;            //2
    [SerializeField] GameObject inventoryCanvas;       //3
    [SerializeField] GameObject settingsCanvas;        //4
    [SerializeField] GameObject configCanvas;          //5
    [SerializeField] GameObject saveBeforeExitCanvas;  //6
    [SerializeField] GameObject deathScreen;           //7
    [SerializeField] GameObject saveSlotCanvas;        //8

    public void Settings()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            Camera cam = Camera.main;
            PlayerCamScript camScript = cam.GetComponent<PlayerCamScript>();

            if (!fadeCanvas.activeSelf && !errorCanvas.activeSelf && !deathScreen.activeSelf)
            {

                if (settingsCanvas.activeSelf || inventoryCanvas.activeSelf)
                {

                    camScript.disableCam = false;
                    mainCanvas.SetActive(true);
                    TurnOffOtherCanvases(2);
                    Time.timeScale = 1f;

                }
                else
                {

                    camScript.disableCam = true;
                    settingsCanvas.SetActive(true);
                    TurnOffOtherCanvases(4);
                    Time.timeScale = 0f;

                }

            }

        }
        else
        {

            if (settingsCanvas.activeSelf || saveSlotCanvas.activeSelf)
            {

                mainCanvas.SetActive(true);
                TurnOffOtherCanvases(2);

            }
            else
            {

                settingsCanvas.SetActive(true);
                TurnOffOtherCanvases(4);

            }

        }

    }

    public void Inventory()
    {

        Camera cam = Camera.main;
        PlayerCamScript camScript = cam.GetComponent<PlayerCamScript>();

        if (inventoryCanvas.activeSelf)
        {

            camScript.disableCam = false;
            mainCanvas.SetActive(true);
            TurnOffOtherCanvases(2);

        }
        else
        {

            camScript.disableCam = true;
            inventoryCanvas.SetActive(true);
            TurnOffOtherCanvases(3);

        }

    }

    public void MenuStart()
    {

        if (mainCanvas.activeSelf)
        {

            saveSlotCanvas.SetActive(true);
            TurnOffOtherCanvases(8);

        }
        else
        {

            mainCanvas.SetActive(true);
            TurnOffOtherCanvases(2);

        }

    }

    public void Fade(int phase)
    {

        if (phase == 0)
        {

            fadeCanvas.SetActive(true);
            TurnOffOtherCanvases(0);

        }
        else if (phase == 1)
        {

            mainCanvas.SetActive(true);

        }
        else if (phase == 2)
        {

            TurnOffOtherCanvases(2);

        }
        else
        {

            Debug.Log("Index out of range! " + phase);

        }

    }

    public void Error()
    {

        Camera cam = Camera.main;
        PlayerCamScript camScript = cam.GetComponent<PlayerCamScript>();

        if (!errorCanvas.activeSelf)
        {

            camScript.disableCam = true;
            errorCanvas.SetActive(true);
            TurnOffOtherCanvases(1);
            Time.timeScale = 0f;

        }
        else
        {

            camScript.disableCam = false;
            mainCanvas.SetActive(true);
            TurnOffOtherCanvases(2);
            Time.timeScale = 1f;

        }

    }

    public void Config()
    {

        if (!configCanvas.activeSelf)
        {

            configCanvas.SetActive(true);
            TurnOffOtherCanvases(5);

        }
        else
        {

            settingsCanvas.SetActive(true);
            TurnOffOtherCanvases(4);

        }

    }

    public void SaveBeforeExit()
    {

        if (settingsCanvas.activeSelf)
        {

            saveBeforeExitCanvas.SetActive(true);
            TurnOffOtherCanvases(6);

        }
        else
        {

            settingsCanvas.SetActive(true);
            TurnOffOtherCanvases(4);

        }

    }

    public void Death()
    {

        Camera cam = Camera.main;
        PlayerCamScript camScript = cam.GetComponent<PlayerCamScript>();
        camScript.disableCam = true;
        deathScreen.SetActive(true);
        TurnOffOtherCanvases(7);

    }

    private void TurnOffOtherCanvases(int index)
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            if (index == 0)
            {

                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else if (index == 1)
            {

                fadeCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else if (index == 2)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else if (index == 3)
            {

                Debug.Log("Canvas not in this scene! " + index);

            }
            else if (index == 4)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else if (index == 5)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else if (index == 6)
            {

                Debug.Log("Canvas not in this scene! " + index);

            }
            else if (index == 7)
            {

                Debug.Log("Canvas not in this scene! " + index);

            }
            else if (index == 8)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);

            }
            else if (index == 9)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveSlotCanvas.SetActive(false);

            }
            else
            {

                Debug.Log("Index out of range! " + index);

            }

        }
        else
        {

            if (index == 0)
            {

                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 1)
            {

                fadeCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 2)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 3)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 4)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 5)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 6)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else if (index == 7)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);

            }
            else if (index == 8)
            {

                Debug.Log("Canvas not in this scene! " + index);

            }
            else if (index == 9)
            {

                fadeCanvas.SetActive(false);
                errorCanvas.SetActive(false);
                mainCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                settingsCanvas.SetActive(false);
                configCanvas.SetActive(false);
                saveBeforeExitCanvas.SetActive(false);
                deathScreen.SetActive(false);

            }
            else
            {

                Debug.Log("Index out of range! " + index);

            }

        }

    }

    public bool IsCanvasActive(int index)
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            if (index == 0)
            {

                if (fadeCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 1)
            {

                if (errorCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 2)
            {

                if (mainCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 3)
            {

                Debug.Log("Canvas not in this scene! " + index);
                return false;

            }
            else if (index == 4)
            {

                if (settingsCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 5)
            {

                if (configCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 6)
            {

                Debug.Log("Canvas not in this scene! " + index);
                return false;

            }
            else if (index == 7)
            {

                Debug.Log("Canvas not in this scene! " + index);
                return false;

            }
            else if (index == 8)
            {

                if (saveSlotCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else
            {

                Debug.Log("Index out of range! " + index);
                return false;

            }

        }
        else
        {

            if (index == 0)
            {

                if (fadeCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 1)
            {

                if (errorCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 2)
            {

                if (mainCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 3)
            {

                if (inventoryCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 4)
            {

                if (settingsCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 5)
            {

                if (configCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 6)
            {

                if (saveBeforeExitCanvas.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 7)
            {

                if (deathScreen.activeSelf)
                {

                    return true;

                }
                else
                {

                    return false;

                }

            }
            else if (index == 8)
            {

                Debug.Log("Canvas not in this scene! " + index);
                return false;

            }
            else
            {

                Debug.Log("Index out of range! " + index);
                return false;

            }

        }

    }

}
