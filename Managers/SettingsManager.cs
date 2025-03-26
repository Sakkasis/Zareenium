using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] List<GameObject> gameSettingsList;
    [SerializeField] List<GameObject> audioSettingsList;
    [SerializeField] List<GameObject> graphicsSettingsList;
    [SerializeField] List<GameObject> controlsSettingsList;

    [SerializeField] TextMeshProUGUI xSensitivityText;
    [SerializeField] TextMeshProUGUI ySensitivityText;
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;

    [field: NonSerialized]
    public int openCategory;

    private void LateUpdate()
    {

        MouseSensitivityCheckVoid();

    }

    public void ChangeCategory(int categoryInt)
    {

        openCategory = categoryInt;

        if (categoryInt == 1)
        {

            foreach (GameObject uiObject in gameSettingsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in audioSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in graphicsSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in controlsSettingsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 2)
        {

            foreach (GameObject uiObject in gameSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in audioSettingsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in graphicsSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in controlsSettingsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 3)
        {

            foreach (GameObject uiObject in gameSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in audioSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in graphicsSettingsList)
            {

                uiObject.SetActive(true);

            }

            foreach (GameObject uiObject in controlsSettingsList)
            {

                uiObject.SetActive(false);

            }

        }
        else if (categoryInt == 4)
        {

            foreach (GameObject uiObject in gameSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in audioSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in graphicsSettingsList)
            {

                uiObject.SetActive(false);

            }

            foreach (GameObject uiObject in controlsSettingsList)
            {

                uiObject.SetActive(true);

            }

        }
        else
        {

            Debug.LogError("!ERROR! SettingsManager ChangeCategory categoryInt index out of range! " + categoryInt.ToString());

        }

    }

    private void MouseSensitivityCheckVoid()
    {

        int xSensitivity = (int)xSensitivitySlider.value;
        int ySensitivity = (int)ySensitivitySlider.value;

        xSensitivityText.SetText(xSensitivity.ToString());
        ySensitivityText.SetText(ySensitivity.ToString());

        PlayerCamScript playerCamScript = Camera.main.GetComponent<PlayerCamScript>();

        playerCamScript.xMouseSensitivity = xSensitivitySlider.value;
        playerCamScript.yMouseSensitivity = ySensitivitySlider.value;

    }

}
