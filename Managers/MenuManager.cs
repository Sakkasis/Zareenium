using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [Header("Misc")]
    [SerializeField] GameObject subTitleObject;

    IDataService DataService = new JsonDataService();
    const string subTitleTextPromptsFileName = "TextPrompts";

    void Start()
    {

        SubTitleRandomizer();

    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Settings"))
        {

            CanvasManager canvasScript = gameObject.GetComponent<CanvasManager>();
            canvasScript.Settings();

        }

    }

    public void SlotSelectionVoid()
    {

        CanvasManager canvasScript = gameObject.GetComponent<CanvasManager>();
        canvasScript.MenuStart();

    }

    public void OpenSettingsVoid()
    {

        CanvasManager canvasScript = gameObject.GetComponent<CanvasManager>();
        canvasScript.Settings();

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
