using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConfigManager : MonoBehaviour
{

    [Header("Conifg")]
    public bool menuOpen = false;
    [SerializeField] GameObject configMenuButton;
    [SerializeField] GameObject configCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject dataTypeInputObj;
    [SerializeField] GameObject dataCommandInputObj;
    [SerializeField] GameObject promptFieldObj;
    [SerializeField] GameObject textPromptPrefab;
    public TextMeshProUGUI confirmCommandText;

    TextMeshProUGUI dataTypeInput;
    TextMeshProUGUI dataCommandInput;
    List<GameObject> dataPromptsList = new List<GameObject>();
    List<GameObject> enemiesList = new List<GameObject>();

    [field: NonSerialized] public PlayerManager pScript;
    [field: NonSerialized] public PlayerCamScript cScript;
    [field: NonSerialized] public List<NormEnemyBehavior> aiScripts = new List<NormEnemyBehavior>();

    ConfigLogic logicScript = new ConfigLogic();
    IConfigService ConfigService = new ConfigService();

    private void Awake()
    {

        logicScript.InitializeConfigDataFile();
        dataTypeInput = dataTypeInputObj.GetComponent<TextMeshProUGUI>();
        dataCommandInput = dataCommandInputObj.GetComponent<TextMeshProUGUI>();
        configMenuButton.SetActive(logicScript.ConfigEnabled());

    }

    public void ConfigMenu()
    {

        menuOpen = !menuOpen;
        SetDataPrompts();
        configCanvas.SetActive(menuOpen);
        settingsCanvas.SetActive(!menuOpen);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            MenuManager menuScript = gameObject.GetComponent<MenuManager>();
            menuScript.settingsOpenBool = !menuOpen;

        }
        else
        {

            pScript = GameObject.FindGameObjectWithTag("bean").GetComponent<PlayerManager>();
            cScript = Camera.main.GetComponent<PlayerCamScript>();
            enemiesList.Clear();
            enemiesList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            aiScripts.Clear();
            for (int i = 0; i < enemiesList.Count; i++)
            {
                aiScripts.Add(enemiesList[i].GetComponent<NormEnemyBehavior>());
            }
            UIManager uiScript = gameObject.GetComponent<UIManager>();
            uiScript.settingsOpen = !menuOpen;

        }

    }

    public void SetDataPrompts()
    {

        List<string> prompts = ConfigService.FormatAllData();

        if (dataPromptsList.Count != 0)
        {
            for (int i = 0; i < dataPromptsList.Count; i++)
            {
                Destroy(dataPromptsList[i]);
            }
        }

        for (int i = 0; i < prompts.Count; i++)
        {

            GameObject textPrompt = GameObject.Instantiate(textPromptPrefab);
            textPrompt.transform.SetParent(promptFieldObj.transform);
            TextMeshProUGUI textField = textPrompt.GetComponent<TextMeshProUGUI>();
            textField.SetText(prompts[i]);
            textField.name = "textPrompt" + i;
            dataPromptsList.Add(textPrompt);

        }

    }

    public void ExecuteCommand()
    {

        string dataType = dataTypeInput.text;
        string dataCommand = dataCommandInput.text;
        List<string> prompts = ConfigService.SetDataByCommand(dataCommand, dataType, pScript, cScript, aiScripts, this);

        if (dataPromptsList.Count != 0)
        {
            for (int i = 0; i < dataPromptsList.Count; i++)
            {
                Destroy(dataPromptsList[i]);
            }
        }

        for (int i = 0; i < prompts.Count; i++)
        {

            GameObject textPrompt = GameObject.Instantiate(textPromptPrefab);
            textPrompt.transform.SetParent(promptFieldObj.transform);
            TextMeshProUGUI textField = textPrompt.GetComponent<TextMeshProUGUI>();
            textField.SetText(prompts[i]);
            textField.name = "textPrompt" + i;
            dataPromptsList.Add(textPrompt);

        }

    }

}

[Serializable]
public class ConfigLogic
{

    const bool Config = true;
    const string configFileName = "ConfigData";
    const string playerFileName = "PlayerData";
    const string enemyFileName = "EnemyData";
    IDataService DataService = new JsonDataService();
    IDataClasses DataClasses = new DataClasses();

    public bool ConfigEnabled()
    {
        return Config;
    }

    public void InitializeConfigDataFile()
    {

        ConfigData data = new ConfigData();
        data.ConfigManager = Config;
        SaveManagerData managerData = DataClasses.SaveManagerDataClass();

        if (DataService.DoesFileExist("PlayerData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1)))
        {

            PlayerData playerData = DataClasses.PlayerDataClass(managerData.currentlySelectedSaveSlot - 1);
            data.pWalkSpeed = playerData.walkSpeed;
            data.pRunSpeed = playerData.runSpeed;
            data.pJumpHeight = playerData.jumpHeight;
            data.pGravity = playerData.gravity;
            data.pHealth = playerData.health;
            data.pMana = playerData.mana;
            data.pMaxHealth = playerData.maxHealth;
            data.pMaxMana = playerData.maxMana;
            data.pHpRegenAmount = playerData.hpRegenAmount;
            data.pManaRegenAmount = playerData.manaRegenAmount;
            data.pHpRegenCooldown = playerData.hpRegenCooldown;
            data.pManaRegenCooldown = playerData.manaRegenCooldown;
            data.pDamageAmount = playerData.damageAmount;
            data.pCritRate = playerData.critRate;
            data.pCritDamage = playerData.critDamage;
            data.pAttackCooldown = playerData.attackCooldown;
            data.pAttackManaCost = playerData.attackManaCost;

            data.playerPositionX = playerData.playerPositionX;
            data.playerPositionY = playerData.playerPositionY;
            data.playerPositionZ = playerData.playerPositionZ;

            data.playerRotationX = playerData.playerRotationX;
            data.playerRotationY = playerData.playerRotationY;
            data.playerRotationZ = playerData.playerRotationZ;

            data.camPositionX = playerData.camPositionX;
            data.camPositionY = playerData.camPositionY;
            data.camPositionZ = playerData.camPositionZ;

            data.camRotationX = playerData.camRotationX;
            data.camRotationY = playerData.camRotationY;
            data.camRotationZ = playerData.camRotationZ;

        }
        else
        {

            GameObject bean = GameObject.FindGameObjectWithTag("bean");
            Camera cam = Camera.main;

            if (bean != null)
            {

                PlayerManager playerScript = bean.GetComponent<PlayerManager>();

                data.pWalkSpeed = playerScript.walkSpeed;
                data.pRunSpeed = playerScript.runSpeed;
                data.pJumpHeight = playerScript.jumpHeight;
                data.pGravity = playerScript.gravity;
                data.pHealth = playerScript.health;
                data.pMana = playerScript.mana;
                data.pMaxHealth = playerScript.maxHealth;
                data.pMaxMana = playerScript.maxMana;
                data.pHpRegenAmount = playerScript.hpRegenAmount;
                data.pManaRegenAmount = playerScript.manaRegenAmount;
                data.pHpRegenCooldown = playerScript.hpRegenCooldown;
                data.pManaRegenCooldown = playerScript.manaRegenCooldown;
                data.pDamageAmount = playerScript.damageAmount;
                data.pCritRate = playerScript.critRate;
                data.pCritDamage = playerScript.critDamage;
                data.pAttackCooldown = playerScript.attackCooldown;
                data.pAttackManaCost = playerScript.attackManaCost;

                data.playerPositionX = bean.transform.localPosition.x;
                data.playerPositionY = bean.transform.localPosition.y;
                data.playerPositionZ = bean.transform.localPosition.z;

                data.playerRotationX = bean.transform.localRotation.x;
                data.playerRotationY = bean.transform.localRotation.y;
                data.playerRotationZ = bean.transform.localRotation.z;

                if (cam != null)
                {

                    data.camPositionX = cam.transform.localPosition.x;
                    data.camPositionY = cam.transform.localPosition.y;
                    data.camPositionZ = cam.transform.localPosition.z;

                    data.camRotationX = cam.transform.localRotation.x;
                    data.camRotationY = cam.transform.localRotation.y;
                    data.camRotationZ = cam.transform.localRotation.z;

                }

            }
            else
            {

                data.pWalkSpeed = 5f;
                data.pRunSpeed = 10f;
                data.pJumpHeight = 2f;
                data.pGravity = 9.81f;
                data.pHealth = 100f;
                data.pMana = 100f;
                data.pMaxHealth = 100f;
                data.pMaxMana = 100f;
                data.pHpRegenAmount = 1f;
                data.pManaRegenAmount = 2f;
                data.pHpRegenCooldown = 0.4f;
                data.pManaRegenCooldown = 0.4f;
                data.pDamageAmount = 20f;
                data.pCritRate = 50;
                data.pCritDamage = 50f;
                data.pAttackCooldown = 0.5f;
                data.pAttackManaCost = 10f;

                data.playerPositionX = 0f;
                data.playerPositionY = 1.2f;
                data.playerPositionZ = 1f;

                data.playerRotationX = 0f;
                data.playerRotationY = 0f;
                data.playerRotationZ = 0f;

                data.camPositionX = data.playerPositionX;
                data.camPositionY = data.playerPositionY + 0.65f;
                data.camPositionZ = data.playerPositionZ;

                data.camRotationX = data.playerRotationX;
                data.camRotationY = data.playerRotationY;
                data.camRotationZ = data.playerRotationZ;

            }

        }

        if (DataService.DoesFileExist("EnemyData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1)))
        {

            EnemyData enemyData = DataClasses.EnemyDataClass(managerData.currentlySelectedSaveSlot - 1);
            int numOfEnemies = enemyData.health.Count;

            data.eHealth.Clear();
            data.eMaxHealth.Clear();
            data.eMana.Clear();
            data.eMaxMana.Clear();
            data.eDoesAIUseMagic.Clear();
            data.eDoesAIPatrol.Clear();
            data.ePatrolWait.Clear();
            data.ePatrolRouteInt.Clear();
            data.ePatrolPointInt.Clear();

            data.enemyPositionX.Clear();
            data.enemyPositionY.Clear();
            data.enemyPositionZ.Clear();

            data.enemyRotationX.Clear();
            data.enemyRotationY.Clear();
            data.enemyRotationZ.Clear();

            data.eHealth.AddRange(new float[numOfEnemies]);
            data.eMaxHealth.AddRange(new float[numOfEnemies]);
            data.eMana.AddRange(new float[numOfEnemies]);
            data.eMaxMana.AddRange(new float[numOfEnemies]);
            data.eDoesAIUseMagic.AddRange(new bool[numOfEnemies]);
            data.eDoesAIPatrol.AddRange(new bool[numOfEnemies]);
            data.ePatrolWait.AddRange(new float[numOfEnemies]);
            data.ePatrolRouteInt.AddRange(new int[numOfEnemies]);
            data.ePatrolPointInt.AddRange(new int[numOfEnemies]);

            data.enemyPositionX.AddRange(new float[numOfEnemies]);
            data.enemyPositionY.AddRange(new float[numOfEnemies]);
            data.enemyPositionZ.AddRange(new float[numOfEnemies]);

            data.enemyRotationX.AddRange(new float[numOfEnemies]);
            data.enemyRotationY.AddRange(new float[numOfEnemies]);
            data.enemyRotationZ.AddRange(new float[numOfEnemies]);

            for (int i = 0; i < numOfEnemies; i++)
            {

                data.eHealth[i] = enemyData.health[i];
                data.eMaxHealth[i] = enemyData.maxHealth[i];
                data.eMana[i] = enemyData.mana[i];
                data.eMaxMana[i] = enemyData.maxMana[i];
                data.eDoesAIUseMagic[i] = enemyData.doesAIUseMagic[i];
                data.eDoesAIPatrol[i] = enemyData.doesAIPatrol[i];
                data.ePatrolWait[i] = enemyData.patrolWait[i];
                data.ePatrolRouteInt[i] = enemyData.patrolRouteInt[i];
                data.ePatrolPointInt[i] = enemyData.patrolRouteInt[i];

                data.enemyPositionX[i] = enemyData.enemyPositionX[i];
                data.enemyPositionY[i] = enemyData.enemyPositionY[i];
                data.enemyPositionZ[i] = enemyData.enemyPositionZ[i];

                data.enemyRotationX[i] = enemyData.enemyRotationX[i];
                data.enemyRotationY[i] = enemyData.enemyRotationY[i];
                data.enemyRotationZ[i] = enemyData.enemyRotationZ[i];

            }

        }
        else
        {

            List<GameObject> enemyObjects = new List<GameObject>();
            enemyObjects.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            if (enemyObjects.Count != 0)
            {

                int numOfEnemies = enemyObjects.Count;

                data.eHealth.Clear();
                data.eMaxHealth.Clear();
                data.eMana.Clear();
                data.eMaxMana.Clear();
                data.eDoesAIUseMagic.Clear();
                data.eDoesAIPatrol.Clear();
                data.ePatrolWait.Clear();
                data.ePatrolRouteInt.Clear();
                data.ePatrolPointInt.Clear();

                data.enemyPositionX.Clear();
                data.enemyPositionY.Clear();
                data.enemyPositionZ.Clear();

                data.enemyRotationX.Clear();
                data.enemyRotationY.Clear();
                data.enemyRotationZ.Clear();

                data.eHealth.AddRange(new float[numOfEnemies]);
                data.eMaxHealth.AddRange(new float[numOfEnemies]);
                data.eMana.AddRange(new float[numOfEnemies]);
                data.eMaxMana.AddRange(new float[numOfEnemies]);
                data.eDoesAIUseMagic.AddRange(new bool[numOfEnemies]);
                data.eDoesAIPatrol.AddRange(new bool[numOfEnemies]);
                data.ePatrolWait.AddRange(new float[numOfEnemies]);
                data.ePatrolRouteInt.AddRange(new int[numOfEnemies]);
                data.ePatrolPointInt.AddRange(new int[numOfEnemies]);

                data.enemyPositionX.AddRange(new float[numOfEnemies]);
                data.enemyPositionY.AddRange(new float[numOfEnemies]);
                data.enemyPositionZ.AddRange(new float[numOfEnemies]);

                data.enemyRotationX.AddRange(new float[numOfEnemies]);
                data.enemyRotationY.AddRange(new float[numOfEnemies]);
                data.enemyRotationZ.AddRange(new float[numOfEnemies]);

                for (int i = 0; i < numOfEnemies; i++)
                {

                    NormEnemyBehavior enemyScript = enemyObjects[i].GetComponent<NormEnemyBehavior>();

                    data.eHealth[i] = enemyScript.health;
                    data.eMaxHealth[i] = enemyScript.maxHealth;
                    data.eMana[i] = enemyScript.mana;
                    data.eMaxMana[i] = enemyScript.maxMana;
                    data.eDoesAIUseMagic[i] = enemyScript.doesAIUseMagic;
                    data.eDoesAIPatrol[i] = enemyScript.doesAIPatrol;
                    data.ePatrolWait[i] = enemyScript.patrolWaitFloat;
                    data.ePatrolRouteInt[i] = enemyScript.patrolRouteInt;
                    data.ePatrolPointInt[i] = enemyScript.patrolRouteInt;

                    data.enemyPositionX[i] = enemyObjects[i].transform.localPosition.x;
                    data.enemyPositionY[i] = enemyObjects[i].transform.localPosition.y;
                    data.enemyPositionZ[i] = enemyObjects[i].transform.localPosition.z;

                    data.enemyRotationX[i] = enemyObjects[i].transform.localRotation.x;
                    data.enemyRotationY[i] = enemyObjects[i].transform.localRotation.y;
                    data.enemyRotationZ[i] = enemyObjects[i].transform.localRotation.z;

                }

            }
            else
            {

                data.eHealth.Clear();
                data.eMaxHealth.Clear();
                data.eMana.Clear();
                data.eMaxMana.Clear();
                data.eDoesAIUseMagic.Clear();
                data.eDoesAIPatrol.Clear();
                data.ePatrolWait.Clear();
                data.ePatrolRouteInt.Clear();
                data.ePatrolPointInt.Clear();

                data.enemyPositionX.Clear();
                data.enemyPositionY.Clear();
                data.enemyPositionZ.Clear();

                data.enemyRotationX.Clear();
                data.enemyRotationY.Clear();
                data.enemyRotationZ.Clear();

                data.eHealth.Add(100f);
                data.eMaxHealth.Add(100f);
                data.eMana.Add(100f);
                data.eMaxMana.Add(100f);
                data.eDoesAIUseMagic.Add(false);
                data.eDoesAIPatrol.Add(true);
                data.ePatrolWait.Add(8f);
                data.ePatrolRouteInt.Add(1);
                data.ePatrolPointInt.Add(0);

                data.enemyPositionX.Add(-6.46f);
                data.enemyPositionY.Add(1.55f);
                data.enemyPositionZ.Add(-4.35f);

                data.enemyRotationX.Add(0f);
                data.enemyRotationY.Add(0f);
                data.enemyRotationZ.Add(0f);

            }

        }

        DataService.SaveData(configFileName, data, true);

    }

    public ConfigData LoadConfigData()
    {

        ConfigData data = new ConfigData();
        SaveManagerData managerData = DataClasses.SaveManagerDataClass();
        
        if (File.Exists("PlayerData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1)) && File.Exists("EnemyData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1)))
        {

            PlayerData pData = DataService.LoadData<PlayerData>("PlayerData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1), false);
            EnemyData eData = DataService.LoadData<EnemyData>("EnemyData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1), false);
            int numOfEnemies = eData.health.Count;

            data.pWalkSpeed = pData.walkSpeed;
            data.pRunSpeed = pData.runSpeed;
            data.pJumpHeight = pData.jumpHeight;
            data.pGravity = pData.gravity;
            data.pHealth = pData.health;
            data.pMana = pData.mana;
            data.pMaxHealth = pData.maxHealth;
            data.pMaxMana = pData.maxMana;
            data.pHpRegenAmount = pData.hpRegenAmount;
            data.pManaRegenAmount = pData.manaRegenAmount;
            data.pHpRegenCooldown = pData.hpRegenCooldown;
            data.pManaRegenCooldown = pData.manaRegenCooldown;
            data.pDamageAmount = pData.damageAmount;
            data.pCritRate = pData.critRate;
            data.pCritDamage = pData.critDamage;
            data.pAttackCooldown = pData.attackCooldown;
            data.pAttackManaCost = pData.attackManaCost;

            data.playerPositionX = pData.playerPositionX;
            data.playerPositionY = pData.playerPositionY;
            data.playerPositionZ = pData.playerPositionZ;

            data.playerRotationX = pData.playerRotationX;
            data.playerRotationY = pData.playerRotationY;
            data.playerRotationZ = pData.playerRotationZ;

            data.camPositionX = pData.camPositionX;
            data.camPositionY = pData.camPositionY;
            data.camPositionZ = pData.camPositionZ;

            data.camRotationX = pData.camRotationX;
            data.camRotationY = pData.camRotationY;
            data.camRotationZ = pData.camRotationZ;

            data.eHealth.Clear();
            data.eMaxHealth.Clear();
            data.eMana.Clear();
            data.eMaxMana.Clear();
            data.eDoesAIUseMagic.Clear();
            data.eDoesAIPatrol.Clear();
            data.ePatrolWait.Clear();
            data.ePatrolRouteInt.Clear();
            data.ePatrolPointInt.Clear();

            data.enemyPositionX.Clear();
            data.enemyPositionY.Clear();
            data.enemyPositionZ.Clear();

            data.enemyRotationX.Clear();
            data.enemyRotationY.Clear();
            data.enemyRotationZ.Clear();

            data.eHealth.AddRange(new float[numOfEnemies]);
            data.eMaxHealth.AddRange(new float[numOfEnemies]);
            data.eMana.AddRange(new float[numOfEnemies]);
            data.eMaxMana.AddRange(new float[numOfEnemies]);
            data.eDoesAIUseMagic.AddRange(new bool[numOfEnemies]);
            data.eDoesAIPatrol.AddRange(new bool[numOfEnemies]);
            data.ePatrolWait.AddRange(new float[numOfEnemies]);
            data.ePatrolRouteInt.AddRange(new int[numOfEnemies]);
            data.ePatrolPointInt.AddRange(new int[numOfEnemies]);

            data.enemyPositionX.AddRange(new float[numOfEnemies]);
            data.enemyPositionY.AddRange(new float[numOfEnemies]);
            data.enemyPositionZ.AddRange(new float[numOfEnemies]);

            data.enemyRotationX.AddRange(new float[numOfEnemies]);
            data.enemyRotationY.AddRange(new float[numOfEnemies]);
            data.enemyRotationZ.AddRange(new float[numOfEnemies]);

            for (int i = 0; i < numOfEnemies; i++)
            {

                data.eHealth[i] = eData.health[i];
                data.eMaxHealth[i] = eData.maxHealth[i];
                data.eMana[i] = eData.mana[i];
                data.eMaxMana[i] = eData.maxMana[i];
                data.eDoesAIUseMagic[i] = eData.doesAIUseMagic[i];
                data.eDoesAIPatrol[i] = eData.doesAIPatrol[i];
                data.ePatrolWait[i] = eData.patrolWait[i];
                data.ePatrolRouteInt[i] = eData.patrolRouteInt[i];
                data.ePatrolPointInt[i] = eData.patrolRouteInt[i];

                data.enemyPositionX[i] = eData.enemyPositionX[i];
                data.enemyPositionY[i] = eData.enemyPositionY[i];
                data.enemyPositionZ[i] = eData.enemyPositionZ[i];

                data.enemyRotationX[i] = eData.enemyRotationX[i];
                data.enemyRotationY[i] = eData.enemyRotationY[i];
                data.enemyRotationZ[i] = eData.enemyRotationZ[i];

            }

        }
        else
        {

            data = DataService.LoadData<ConfigData>(configFileName, true);

        }

        return data;

    }

    public bool SaveConfigData(ConfigData data, PlayerData pData, EnemyData eData)
    {

        if (DataService.SaveData(configFileName, data, true) && DataService.SaveData(playerFileName + DataClasses.SlotFileName(DataClasses.SaveManagerDataClass().currentlySelectedSaveSlot - 1), pData, false) && DataService.SaveData(enemyFileName + DataClasses.SlotFileName(DataClasses.SaveManagerDataClass().currentlySelectedSaveSlot - 1), eData, false))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

}
