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

            if (!fadeCanvas.activeSelf)
            {

                if (settingsCanvas.activeSelf)
                {

                    TurnOffOtherCanvases(2);
                    mainCanvas.SetActive(true);
                    Time.timeScale = 1f;

                }
                else if (configCanvas.activeSelf)
                {

                    TurnOffOtherCanvases(5);
                    configCanvas.SetActive(true);

                }
                else
                {

                    TurnOffOtherCanvases(4);
                    settingsCanvas.SetActive(true);
                    Time.timeScale = 0f;

                }

            }

        }
        else
        {

            if (settingsCanvas.activeSelf)
            {

                TurnOffOtherCanvases(2);
                mainCanvas.SetActive(true);
                Time.timeScale = 1f;

            }
            else
            {

                TurnOffOtherCanvases(4);
                settingsCanvas.SetActive(true);
                Time.timeScale = 0f;

            }

        }

    }

    public void Inventory()
    {

        if (inventoryCanvas.activeSelf)
        {

            TurnOffOtherCanvases(2);
            mainCanvas.SetActive(true);

        }
        else
        {

            TurnOffOtherCanvases(3);
            inventoryCanvas.SetActive(true);

        }

    }

    public void MenuStart()
    {

        if (mainCanvas.activeSelf)
        {

            TurnOffOtherCanvases(8);
            saveSlotCanvas.SetActive(true);

        }
        else
        {

            TurnOffOtherCanvases(2);
            mainCanvas.SetActive(true);

        }

    }

    public void Fade()
    {

        if (!fadeCanvas.activeSelf)
        {

            TurnOffOtherCanvases(0);
            fadeCanvas.SetActive(true);

        }
        else
        {

            TurnOffOtherCanvases(2);
            mainCanvas.SetActive(true);

        }

    }

    public void Error()
    {

        TurnOffOtherCanvases(9);
        errorCanvas.SetActive(true);

    }

    public void Config()
    {

        if (!configCanvas.activeSelf)
        {

            TurnOffOtherCanvases(5);
            configCanvas.SetActive(true);

        }
        else
        {

            TurnOffOtherCanvases(4);
            settingsCanvas.SetActive(true);

        }

    }

    public void SaveBeforeExit()
    {

        if (settingsCanvas.activeSelf)
        {

            TurnOffOtherCanvases(6);
            saveBeforeExitCanvas.SetActive(true);

        }
        else
        {

            TurnOffOtherCanvases(4);
            settingsCanvas.SetActive(true);

        }

    }

    private void TurnOffOtherCanvases(int index)
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
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

        if (SceneManager.GetActiveScene().buildIndex != 0)
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
