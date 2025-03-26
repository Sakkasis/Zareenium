using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [Header("Canvases")]
    [SerializeField] GameObject fadeCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject saveSlotCanvas;
    [SerializeField] GameObject settingsCanvas;

    [Header("Misc")]
    [SerializeField] GameObject subTitleObject;

    [field: NonSerialized]
    public bool settingsOpenBool = false;
    [field: NonSerialized]
    public bool saveSelectionOpenBool = false;

    IDataService DataService = new JsonDataService();
    const string subTitleTextPromptsFileName = "TextPrompts";

    void Start()
    {

        SubTitleRandomizer();

    }

    public void SlotSelectionVoid()
    {

        saveSelectionOpenBool = !saveSelectionOpenBool;

        if (saveSelectionOpenBool)
        {

            mainCanvas.SetActive(false);
            saveSlotCanvas.SetActive(true);

        }
        else
        {

            mainCanvas.SetActive(true);
            saveSlotCanvas.SetActive(false);

        }

    }

    public void OpenSettingsVoid()
    {

        settingsOpenBool = !settingsOpenBool;

        if (settingsOpenBool)
        {

            mainCanvas.SetActive(false);
            settingsCanvas.SetActive(true);

        }
        else
        {

            mainCanvas.SetActive(true);
            settingsCanvas.SetActive(false);

        }

    }

    private void SubTitleRandomizer()
    {

        TextMeshProUGUI subTitleText = subTitleObject.GetComponent<TextMeshProUGUI>();
        SubTitleTextPrompts subTitleTextPromptesClass = new SubTitleTextPrompts();
        subTitleObject.SetActive(true);

        SubTitleTextPrompts data = DataService.LoadData<SubTitleTextPrompts>(subTitleTextPromptsFileName, false);

        subTitleTextPromptesClass.textPrompts.Clear();
        subTitleTextPromptesClass.textPrompts.AddRange(new string[data.textPrompts.Count]);

        for (int i = 0; i < data.textPrompts.Count; i++)
        {

            subTitleTextPromptesClass.textPrompts[i] = data.textPrompts[i];

        }

        int randomInt = UnityEngine.Random.Range(0, subTitleTextPromptesClass.textPrompts.Count);

        subTitleText.SetText(subTitleTextPromptesClass.textPrompts[randomInt]);

    }

}
