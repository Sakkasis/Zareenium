using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class SaveManager : MonoBehaviour
{

    private bool loadingScene = false;

    [Header("Main Menu Only Objects")]
    [SerializeField] GameObject LastSessionNotSavedProperlyObject;
    [SerializeField] GameObject loadingSceneTextObjectI;
    [SerializeField] GameObject loadingSceneTextObjectII;
    [SerializeField] GameObject OCDTextObjectI;
    [SerializeField] GameObject OCDTextObjectII;
    [SerializeField] List<GameObject> lastSaveTimeObjectList;

    [Header("In Game Objects")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject saveBeforeExitCanvas;

    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject mainCamPrefab;

    GameObject gameManager;
    GameObject audioManagerObject;

    SaveLoadConfirmer saveLoadConfirmerScript;
    SettingsManager settingsManagerScript;
    AudioManager audioManagerScript;
    Fader faderScript;

    SaveGameState saveGameStateClass = new SaveGameState();
    LoadSavedGameState loadSavedGameStateClass = new LoadSavedGameState();

    IDataService DataService = new JsonDataService();
    IDataClasses DataClasses = new DataClasses();
    IErrorService ErrorService = new ErrorService();

    const string slotIFileName = "_slot-I";
    const string slotIIFileName = "_slot-II";
    const string slotIIIFileName = "_slot-III";
    const string slotIVFileName = "_slot-IV";
    const string slotVFileName = "_slot-V";

    const string logFilename = "ErrorLog";
    const string saveManagerDataFileName = "SaveManagerData";
    const string slotDataFileName = "SlotData";
    const string playerDataFileName = "PlayerData";
    const string enemyDataFileName = "EnemyData";
    const string settingsDataFileName = "SettingsData";
    const string subTitleTextPromptsFileName = "TextPrompts";
    const string configFileName = "ConfigData";

    private void Awake()
    {

        audioManagerObject = GameObject.FindGameObjectWithTag("AudioManager");
        audioManagerScript = audioManagerObject.GetComponent<AudioManager>();

        InitializeDataFiles();
        ErrorService.LogError(null);

    }

    private void Start()
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();
        SettingsData settingsData = DataClasses.SettingsDataClass();

        if (DataService.LoadData<ConfigData>(configFileName, true).ConfigManager == false)
        {

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {

                gameManager = GameObject.FindGameObjectWithTag("MenuManager");
                settingsManagerScript = gameManager.GetComponent<SettingsManager>();
                faderScript = gameManager.GetComponent<Fader>();
                faderScript.FadeVoid(255f, 0f, 5f, null);

                if (saveManagerData.playersFirstSession == true)
                {

                    saveManagerData.playersFirstSession = false;
                    settingsData.openSettingsTab = 2;
                    settingsManagerScript.ChangeCategory(2);

                }
                else
                {

                    if (settingsData.openSettingsTab == 1)
                    {

                        settingsData.openSettingsTab = 2;
                        settingsManagerScript.ChangeCategory(2);

                    }
                    else
                    {

                        settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                    }

                }

                if (saveManagerData.quitWithoutSavingLastTime == true)
                {

                    saveManagerData.quitWithoutSavingLastTime = false;
                    StartCoroutine(LastSessionEndedWithoutSavingIE());

                }

                SetLastSaveTimeVoid();

            }
            else if (SceneManager.GetActiveScene().buildIndex != 0)
            {

                gameManager = GameObject.FindGameObjectWithTag("UIManager");
                faderScript = gameManager.GetComponent<Fader>();
                saveLoadConfirmerScript = gameObject.GetComponent<SaveLoadConfirmer>();
                settingsManagerScript = gameManager.GetComponent<SettingsManager>();

                faderScript.FadeVoid(255f, 0f, 5f, null);
                settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                if (saveManagerData.loadDataOnSceneLoad == true)
                {

                    LoadSaveOnSceneLoadVoid();
                    saveManagerData.loadDataOnSceneLoad = false;

                }

                if (settingsData.openSettingsTab == 2)
                {

                    settingsData.openSettingsTab = 1;
                    settingsManagerScript.ChangeCategory(1);

                }
                else
                {

                    settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                }

            }

        }
        else
        {

            saveManagerData.currentlySelectedSaveSlot = 1;
            saveManagerData.loadDataOnSceneLoad = false;
            saveManagerData.quitWithoutSavingLastTime = false;
            saveManagerData.savedRecently = true;

            DataService.SaveData(saveManagerDataFileName, saveManagerData, true);

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {

                gameManager = GameObject.FindGameObjectWithTag("MenuManager");
                settingsManagerScript = gameManager.GetComponent<SettingsManager>();
                faderScript = gameManager.GetComponent<Fader>();
                faderScript.FadeVoid(255f, 0f, 5f, null);

                if (settingsData.openSettingsTab == 1)
                {

                    settingsData.openSettingsTab = 2;
                    settingsManagerScript.ChangeCategory(2);

                }
                else
                {

                    settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                }

            }
            else
            {

                gameManager = GameObject.FindGameObjectWithTag("UIManager");
                faderScript = gameManager.GetComponent<Fader>();
                saveLoadConfirmerScript = gameObject.GetComponent<SaveLoadConfirmer>();
                settingsManagerScript = gameManager.GetComponent<SettingsManager>();

                faderScript.FadeVoid(255f, 0f, 5f, null);
                settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                if (settingsData.openSettingsTab == 2)
                {

                    settingsData.openSettingsTab = 1;
                    settingsManagerScript.ChangeCategory(1);

                }
                else
                {

                    settingsManagerScript.ChangeCategory(settingsData.openSettingsTab);

                }

                if (saveManagerData.loadDataOnSceneLoad == true)
                {

                    LoadSaveOnSceneLoadVoid();
                    saveManagerData.loadDataOnSceneLoad = false;

                }
                else
                {

                    SaveGameVoid();

                }

            }

        }

        DataService.SaveData(saveManagerDataFileName, saveManagerData, true);
        DataService.SaveData(settingsDataFileName, settingsData, false);

    }

    private void FixedUpdate()
    {

        if (Input.GetButtonDown("Settings"))
        {

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {

                UIManager uiManagerScript = gameManager.GetComponent<UIManager>();

                if (uiManagerScript.settingsOpen)
                {

                    loadSavedGameStateClass.LoadSettingsDataBool();

                }
                else
                {

                    saveGameStateClass.SaveSettingsDataBool();

                }

            }
            else
            {

                MenuManager menuManagerScript = gameManager.GetComponent<MenuManager>();

                if (menuManagerScript.settingsOpenBool)
                {

                    loadSavedGameStateClass.LoadSettingsDataBool();

                }
                else
                {

                    saveGameStateClass.SaveSettingsDataBool();

                }

            }

        }

    }

    private void OnApplicationQuit()
    {

        if (DataService.LoadData<ConfigData>(configFileName, true).ConfigManager == false)
        {

            SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

            if (saveManagerData.savedRecently == false && SceneManager.GetActiveScene().buildIndex != 0)
            {

                saveManagerData.currentlySelectedSaveSlot = 0;
                saveManagerData.quitWithoutSavingLastTime = true;

                saveGameStateClass.SaveManagerDataBool(saveManagerData);

            }

            if (saveManagerData.savedRecently == true)
            {

                saveManagerData.savedRecently = false;
                saveManagerData.currentlySelectedSaveSlot = 0;
                saveGameStateClass.SaveManagerDataBool(saveManagerData);

            }

        }

    }

    private void InitializeDataFiles()
    {

        try
        {

            if (DataService.DoesFileExist(logFilename) == false)
            {

                ErrorLog errorLogClass = new ErrorLog();
                errorLogClass.ErrorMessages.Clear();
                errorLogClass.ErrorMessages.AddRange(new string[1]);
                errorLogClass.ErrorMessages[0] = "This is the error log file! Please do not tamper with it, as it logs any and all errors and the relevant info about them into this file, if you experience an error in the game please send this file to the developer, thank you for your cooperation!";
                DataService.SaveData("ErrorLog", errorLogClass, false);

            }

            if (DataService.DoesFileExist(saveManagerDataFileName) == false)
            {

                SaveManagerData saveManagerData = new SaveManagerData();

                saveManagerData.playersFirstSession = true;
                saveManagerData.currentlySelectedSaveSlot = 0;
                saveManagerData.loadDataOnSceneLoad = false;
                saveManagerData.savedRecently = true;
                saveManagerData.quitWithoutSavingLastTime = false;

                DataService.SaveData(saveManagerDataFileName, saveManagerData, true);

            }

            if (DataService.DoesFileExist(slotDataFileName) == false)
            {

                SlotData slotDataClass = new SlotData();
                slotDataClass.fileName.Clear();
                slotDataClass.hasPlayerUsedSlotBool.Clear();
                slotDataClass.playersFirstLoad.Clear();
                slotDataClass.savedSceneInt.Clear();
                slotDataClass.timeOfLastSave.Clear();
                slotDataClass.numOfEnemiesInScene.Clear();

                slotDataClass.fileName.AddRange(new string[5]);
                slotDataClass.hasPlayerUsedSlotBool.AddRange(new bool[5]);
                slotDataClass.playersFirstLoad.AddRange(new bool[5]);
                slotDataClass.savedSceneInt.AddRange(new int[5]);
                slotDataClass.timeOfLastSave.AddRange(new string[5]);
                slotDataClass.numOfEnemiesInScene.AddRange(new int[5]);

                slotDataClass.fileName[0] = slotIFileName;
                slotDataClass.fileName[1] = slotIIFileName;
                slotDataClass.fileName[2] = slotIIIFileName;
                slotDataClass.fileName[3] = slotIVFileName;
                slotDataClass.fileName[4] = slotVFileName;

                for (int i = 0; i < 5; i++)
                {

                    slotDataClass.hasPlayerUsedSlotBool[i] = false;
                    slotDataClass.playersFirstLoad[i] = true;
                    slotDataClass.savedSceneInt[i] = 1;
                    slotDataClass.timeOfLastSave[i] = "Time: HH:MM.s | Date: DD/MM/YYYY";
                    slotDataClass.numOfEnemiesInScene[i] = 1;

                }
                DataService.SaveData(slotDataFileName, slotDataClass, true);

            }

            if (DataService.DoesFileExist(settingsDataFileName) == false)
            {

                SettingsData settingsDataClass = new SettingsData();
                settingsDataClass.openSettingsTab = 2;
                settingsDataClass.xMouseSensitivity = 6f;
                settingsDataClass.yMouseSensitivity = 6f;
                DataService.SaveData(settingsDataFileName, settingsDataClass, false);

            }

            if (DataService.DoesFileExist(subTitleTextPromptsFileName) == false)
            {

                SubTitleTextPrompts subTitleTextPromptsClass = new SubTitleTextPrompts();
                subTitleTextPromptsClass.textPrompts.Clear();
                subTitleTextPromptsClass.textPrompts.AddRange(new string[7]);
                subTitleTextPromptsClass.textPrompts[0] = "Beandom, Kingdom\nhehe get it?";
                subTitleTextPromptsClass.textPrompts[1] = "THE POWER OF GOLK COMPELS YOU!";
                subTitleTextPromptsClass.textPrompts[2] = "Spill oil brothers!\n-SES Harbinger of Judgment";
                subTitleTextPromptsClass.textPrompts[3] = "The beans looking kinda sus";
                subTitleTextPromptsClass.textPrompts[4] = "Did you ever hear the tragedy of Darth Plagueis the Wise?";
                subTitleTextPromptsClass.textPrompts[5] = "Yo mama so fat\n<color=red>!ERROR! index out of range</color>";
                subTitleTextPromptsClass.textPrompts[6] = "FUN FACT!\nYou can go edit and even add new randomized text prompts on a file named TextPrompts.json in\nC:/Users/YourUsername/AppData/LocalLow/Sakkasis/zareenium";
                DataService.SaveData(subTitleTextPromptsFileName, subTitleTextPromptsClass, false);

            }

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {

                SlotData slotData = DataClasses.SlotDataClass();
                SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

                if (DataService.DoesFileExist(playerDataFileName + slotData.fileName[saveManagerData.currentlySelectedSaveSlot - 1]) == false)
                {

                    saveGameStateClass.SavePlayerDataBool(saveManagerData.currentlySelectedSaveSlot - 1);

                }

                if (DataService.DoesFileExist(enemyDataFileName + slotData.fileName[saveManagerData.currentlySelectedSaveSlot - 1]) == false)
                {

                    saveGameStateClass.SaveEnemyDataBool(saveManagerData.currentlySelectedSaveSlot - 1);

                }

            }

        }
        catch (Exception e)
        {

            ErrorService.LogError(e);

        }

    }

    public void ChooseSaveSlotVoid(int chosenSlotLocalInt)
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

        if (loadingScene == false)
        {

            loadingScene = true;
            saveManagerData.currentlySelectedSaveSlot = chosenSlotLocalInt;
            saveGameStateClass.SaveManagerDataBool(saveManagerData);
            SlotSelectedLoadSceneVoid();

        }

    }

    private void SlotSelectedLoadSceneVoid()
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();
        SlotData slotData = DataClasses.SlotDataClass();

        if (saveManagerData.currentlySelectedSaveSlot == 1 || slotData.hasPlayerUsedSlotBool[saveManagerData.currentlySelectedSaveSlot - 2] == true)
        {

            slotData.hasPlayerUsedSlotBool[saveManagerData.currentlySelectedSaveSlot - 1] = true;

            if (slotData.playersFirstLoad[saveManagerData.currentlySelectedSaveSlot - 1] == true)
            {

                slotData.playersFirstLoad[saveManagerData.currentlySelectedSaveSlot - 1] = false;
                saveManagerData.loadDataOnSceneLoad = false;

            }
            else
            {

                saveManagerData.loadDataOnSceneLoad = true;

            }

            if (DataService.SaveData(slotDataFileName, slotData, true) == true && DataService.SaveData(saveManagerDataFileName, saveManagerData, true) == true)
            {

                faderScript.FadeVoid(0f, 255f, 5f, slotData.savedSceneInt[saveManagerData.currentlySelectedSaveSlot - 1]);

            }

        }
        else
        {

            loadingScene = false;
            StartCoroutine(OCDTextIE());

        }

    }

    public void SaveGameVoid()
    {

        long startTime = DateTime.Now.Ticks;

        if (SaveGameStateBool() == true)
        {

            saveLoadConfirmerScript.ConfirmSaveVoid(null, startTime);

        }

    }

    public bool SaveGameStateBool()
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

        if (saveGameStateClass.SaveSlotDataBool(saveManagerData.currentlySelectedSaveSlot - 1) == true && saveGameStateClass.SaveManagerDataBool(saveManagerData) == true)
        {

            if (saveGameStateClass.SavePlayerDataBool(saveManagerData.currentlySelectedSaveSlot - 1) == true && saveGameStateClass.SaveEnemyDataBool(saveManagerData.currentlySelectedSaveSlot - 1) == true && saveGameStateClass.SaveSettingsDataBool() == true && audioManagerScript.SaveAudioSettings() == true)
            {

                StartCoroutine(SavedRecentlyTimerIE());
                return true;

            }
            else
            {

                return false;

            }

        }
        else
        {

            return false;

        }

    }

    public void LoadSaveOnSceneLoadVoid()
    {

        long startTime = DateTime.Now.Ticks;
        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

        if (loadSavedGameStateClass.LoadSettingsDataBool() == true && loadSavedGameStateClass.LoadPlayerDataBool(playerPrefab, mainCamPrefab, saveManagerData.currentlySelectedSaveSlot - 1) == true && loadSavedGameStateClass.LoadEnemyDataBool(saveManagerData.currentlySelectedSaveSlot - 1, enemyPrefab) == true)
        {

            saveLoadConfirmerScript.ConfirmLoadVoid(null, startTime);

        }

    }

    private void SetLastSaveTimeVoid()
    {

        for (int i = 0; i < lastSaveTimeObjectList.Count; i++)
        {

            TextMeshProUGUI lastSaveTimeTextLocal = lastSaveTimeObjectList[i].GetComponent<TextMeshProUGUI>();
            lastSaveTimeTextLocal.SetText(DataClasses.SlotDataClass().timeOfLastSave[i]);

        }

    }

    IEnumerator OCDTextIE()
    {

        OCDTextObjectI.SetActive(true);
        OCDTextObjectII.SetActive(true);

        yield return new WaitForSecondsRealtime(8f);

        OCDTextObjectI.SetActive(false);
        OCDTextObjectII.SetActive(true);

    }

    IEnumerator SavedRecentlyTimerIE()
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();

        saveManagerData.savedRecently = true;
        saveGameStateClass.SaveManagerDataBool(saveManagerData);

        yield return new WaitForSecondsRealtime(20f);

        saveManagerData.savedRecently = false;
        saveGameStateClass.SaveManagerDataBool(saveManagerData);

    }

    IEnumerator LastSessionEndedWithoutSavingIE()
    {

        LastSessionNotSavedProperlyObject.SetActive(true);
        yield return new WaitForSecondsRealtime(10f);
        LastSessionNotSavedProperlyObject.SetActive(false);

    }

    public void QuitGameVoid(bool saveBeforeExit)
    {

        long startTime = DateTime.Now.Ticks;

        if (saveBeforeExit == true)
        {

            if (SaveGameStateBool() == true)
            {

                saveLoadConfirmerScript.ConfirmSaveVoid(null, startTime);
                StartCoroutine(QuitGameIE(true));

            }

        }
        else
        {

            StartCoroutine(QuitGameIE(true));

        }

    }

    public void ExitToMenuVoid(bool saveBeforeExit)
    {

        long startTime = DateTime.Now.Ticks;

        if (saveBeforeExit == true)
        {

            if (SaveGameStateBool() == true)
            {

                saveLoadConfirmerScript.ConfirmSaveVoid(null, startTime);
                StartCoroutine(QuitGameIE(false));

            }

        }
        else
        {

            StartCoroutine(QuitGameIE(false));

        }

    }

    IEnumerator QuitGameIE(bool quitGame)
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();
        GameObject bean = GameObject.FindGameObjectWithTag("bean");
        PlayerManager playerScript = bean.GetComponent<PlayerManager>();

        faderScript.FadeSetImageAlpha(0f, 255f, false);

        if (SceneManager.GetActiveScene().buildIndex != 0 && playerScript.playerIsDead == false)
        {

            if (saveManagerData.savedRecently == true)
            {

                if (quitGame == true)
                {

                    faderScript.FadeVoid(null, null, 3f, null);
                    yield return new WaitForSecondsRealtime(3f);
                    saveManagerData.savedRecently = true;
                    saveManagerData.currentlySelectedSaveSlot = 0;
                    saveManagerData.quitWithoutSavingLastTime = false;
                    saveGameStateClass.SaveManagerDataBool(saveManagerData);
                    yield return new WaitForFixedUpdate();
                    DOTween.KillAll();
                    Application.Quit();

                }
                else
                {

                    faderScript.FadeVoid(null, null, 3f, 0);
                    yield return new WaitForSecondsRealtime(2.9f);
                    saveManagerData.savedRecently = true;
                    saveManagerData.currentlySelectedSaveSlot = 0;
                    saveManagerData.quitWithoutSavingLastTime = false;
                    saveGameStateClass.SaveManagerDataBool(saveManagerData);
                    DOTween.KillAll();

                }

            }
            else
            {

                if (quitGame == true && saveBeforeExitCanvas.activeSelf == true)
                {

                    if (saveGameStateClass.SaveSlotDataBool(saveManagerData.currentlySelectedSaveSlot - 1) == true && saveGameStateClass.SaveSettingsDataBool() == true && audioManagerScript.SaveAudioSettings() == true)
                    {

                        faderScript.FadeVoid(null, null, 3f, null);
                        yield return new WaitForSecondsRealtime(3f);
                        saveManagerData.savedRecently = true;
                        saveManagerData.currentlySelectedSaveSlot = 0;
                        saveManagerData.quitWithoutSavingLastTime = false;
                        saveGameStateClass.SaveManagerDataBool(saveManagerData);
                        yield return new WaitForFixedUpdate();
                        DOTween.KillAll();
                        Application.Quit();

                    }
                    else
                    {

                        faderScript.FadeVoid(null, null, 3f, null);
                        yield return new WaitForSecondsRealtime(3f);
                        saveManagerData.savedRecently = true;
                        saveManagerData.currentlySelectedSaveSlot = 0;
                        saveManagerData.quitWithoutSavingLastTime = true;
                        saveGameStateClass.SaveManagerDataBool(saveManagerData);
                        yield return new WaitForFixedUpdate();
                        DOTween.KillAll();
                        Application.Quit();

                    }

                }
                else if (saveBeforeExitCanvas.activeSelf == true)
                {

                    if (saveGameStateClass.SaveSlotDataBool(saveManagerData.currentlySelectedSaveSlot - 1) == true && saveGameStateClass.SaveSettingsDataBool() == true && audioManagerScript.SaveAudioSettings() == true)
                    {

                        faderScript.FadeVoid(null, null, 3f, null);
                        yield return new WaitForSecondsRealtime(2.9f);
                        saveManagerData.savedRecently = true;
                        saveManagerData.currentlySelectedSaveSlot = 0;
                        saveManagerData.quitWithoutSavingLastTime = false;
                        saveGameStateClass.SaveManagerDataBool(saveManagerData);
                        DOTween.KillAll();

                    }
                    else
                    {

                        faderScript.FadeVoid(null, null, 3f, null);
                        yield return new WaitForSecondsRealtime(2.9f);
                        saveManagerData.savedRecently = true;
                        saveManagerData.currentlySelectedSaveSlot = 0;
                        saveManagerData.quitWithoutSavingLastTime = true;
                        saveGameStateClass.SaveManagerDataBool(saveManagerData);
                        DOTween.KillAll();

                    }

                }
                else
                {

                    saveBeforeExitCanvas.SetActive(true);
                    settingsCanvas.SetActive(false);

                }

            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 0 && playerScript.playerIsDead == false)
        {

            faderScript.FadeVoid(null, null, 3f, null);
            yield return new WaitForSecondsRealtime(3f);
            saveManagerData.savedRecently = true;
            saveManagerData.currentlySelectedSaveSlot = 0;
            saveManagerData.quitWithoutSavingLastTime = false;
            saveGameStateClass.SaveSettingsDataBool();
            saveGameStateClass.SaveManagerDataBool(saveManagerData);
            yield return new WaitForFixedUpdate();
            DOTween.KillAll();
            Application.Quit();

        }
        else if (playerScript.playerIsDead == true)
        {

            yield return new WaitForSecondsRealtime(3f);
            saveManagerData.savedRecently = true;
            saveManagerData.currentlySelectedSaveSlot = 0;
            saveManagerData.quitWithoutSavingLastTime = false;
            saveGameStateClass.SaveSettingsDataBool();
            saveGameStateClass.SaveManagerDataBool(saveManagerData);
            yield return new WaitForFixedUpdate();
            DOTween.KillAll();
            SceneManager.LoadScene(0);

        }

    }

    public void LoadSettings()
    {

        loadSavedGameStateClass.LoadSettingsDataBool();

    }

    public void SaveSettings()
    {

        saveGameStateClass.SaveSettingsDataBool();

    }

    public void AutoSave()
    {

        SaveGameStateBool();

    }

    public void RestartFromLastSave()
    {

        SaveManagerData saveManagerData = DataClasses.SaveManagerDataClass();
        SlotData slotData = DataClasses.SlotDataClass();

        saveManagerData.loadDataOnSceneLoad = true;
        saveManagerData.savedRecently = true;

        if (saveGameStateClass.SaveManagerDataBool(saveManagerData) == true)
        {

            faderScript.FadeVoid(0f, 255f, 4f, slotData.savedSceneInt[saveManagerData.currentlySelectedSaveSlot - 1]);

        }

    }

}
