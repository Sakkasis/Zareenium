using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameState
{

    IDataService DataService = new JsonDataService();
    ISystemTime SystemTime = new SystemTime();
    IDataClasses DataClasses = new DataClasses();
    IMathService MathService = new MathService();

    const string saveManagerDataFileName = "SaveManagerData";
    const string slotDataFileName = "SlotData";
    const string playerDataFileName = "PlayerData";
    const string enemyDataFileName = "EnemyData";
    const string settingsDataFileName = "SettingsData";

    public bool SaveManagerDataBool(SaveManagerData saveManagerData)
    {

        SaveManagerData managerDataClass = new SaveManagerData();

        managerDataClass.playersFirstSession = saveManagerData.playersFirstSession;
        managerDataClass.currentlySelectedSaveSlot = saveManagerData.currentlySelectedSaveSlot;
        managerDataClass.loadDataOnSceneLoad = saveManagerData.loadDataOnSceneLoad;
        managerDataClass.savedRecently = saveManagerData.savedRecently;
        managerDataClass.quitWithoutSavingLastTime = saveManagerData.quitWithoutSavingLastTime;

        if (DataService.SaveData(saveManagerDataFileName, managerDataClass, true))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool SaveSlotDataBool(int slotIndex)
    {

        SlotData slotDataClass = DataClasses.SlotDataClass();

        slotDataClass.timeOfLastSave[slotIndex] = SystemTime.FullDateTime();
        slotDataClass.savedSceneInt[slotIndex] = SceneManager.GetActiveScene().buildIndex;

        List<GameObject> enemiesInSceneList = new List<GameObject>();
        enemiesInSceneList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        slotDataClass.numOfEnemiesInScene[slotIndex] = enemiesInSceneList.Count;

        if (DataService.SaveData(slotDataFileName, slotDataClass, true))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool SavePlayerDataBool(int slotIndex)
    {

        PlayerData playerDataClass = new PlayerData();

        Camera camLocal = Camera.main;
        GameObject playerObjectLocal = GameObject.FindGameObjectWithTag("bean");
        PlayerManager playerScriptLocal = playerObjectLocal.GetComponent<PlayerManager>();

        playerDataClass.walkSpeed = playerScriptLocal.walkSpeed;
        playerDataClass.runSpeed = playerScriptLocal.runSpeed;
        playerDataClass.jumpHeight = playerScriptLocal.jumpHeight;
        playerDataClass.gravity = playerScriptLocal.gravity;
        playerDataClass.health = playerScriptLocal.health;
        playerDataClass.maxHealth = playerScriptLocal.maxHealth;
        playerDataClass.mana = playerScriptLocal.mana;
        playerDataClass.maxMana = playerScriptLocal.maxMana;
        playerDataClass.hpRegenAmount = playerScriptLocal.hpRegenAmount;
        playerDataClass.manaRegenAmount = playerScriptLocal.manaRegenAmount;
        playerDataClass.hpRegenCooldown = playerScriptLocal.hpRegenCooldown;
        playerDataClass.manaRegenCooldown = playerScriptLocal.manaRegenCooldown;
        playerDataClass.damageAmount = playerScriptLocal.damageAmount;
        playerDataClass.critRate = playerScriptLocal.critRate;
        playerDataClass.critDamage = playerScriptLocal.critDamage;
        playerDataClass.attackCooldown = playerScriptLocal.attackCooldown;
        playerDataClass.attackManaCost = playerScriptLocal.attackManaCost;

        playerDataClass.playerPositionX = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localPosition.x, 3);
        playerDataClass.playerPositionY = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localPosition.y, 3);
        playerDataClass.playerPositionZ = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localPosition.z, 3);

        playerDataClass.playerRotationX = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localEulerAngles.x, 3);
        playerDataClass.playerRotationY = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localEulerAngles.y, 3);
        playerDataClass.playerRotationZ = MathService.RoundFloatToDecimalPoint(playerObjectLocal.transform.localEulerAngles.z, 3);

        playerDataClass.camPositionX = MathService.RoundFloatToDecimalPoint(camLocal.transform.localPosition.x, 3);
        playerDataClass.camPositionY = MathService.RoundFloatToDecimalPoint(camLocal.transform.localPosition.y, 3);
        playerDataClass.camPositionZ = MathService.RoundFloatToDecimalPoint(camLocal.transform.localPosition.z, 3);

        playerDataClass.camRotationX = MathService.RoundFloatToDecimalPoint(camLocal.transform.localEulerAngles.x, 3);
        playerDataClass.camRotationY = MathService.RoundFloatToDecimalPoint(camLocal.transform.localEulerAngles.y, 3);
        playerDataClass.camRotationZ = MathService.RoundFloatToDecimalPoint(camLocal.transform.localEulerAngles.z, 3);

        if (DataService.SaveData(playerDataFileName + DataClasses.SlotDataClass().fileName[slotIndex], playerDataClass, false))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool SaveEnemyDataBool(int slotIndex)
    {

        EnemyData enemyData = new EnemyData();
        List<GameObject> enemyObjectsListLocal = new List<GameObject>();
        enemyObjectsListLocal.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        int numOfEnemies = enemyObjectsListLocal.Count;

        SlotData slotDataClass = DataClasses.SlotDataClass();

        if (numOfEnemies != 0)
        {

            enemyData.health.Clear();
            enemyData.maxHealth.Clear();
            enemyData.mana.Clear();
            enemyData.maxMana.Clear();
            enemyData.doesAIUseMagic.Clear();
            enemyData.doesAIPatrol.Clear();
            enemyData.patrolWait.Clear();
            enemyData.patrolRouteInt.Clear();
            enemyData.patrolPointInt.Clear();

            enemyData.enemyPositionX.Clear();
            enemyData.enemyPositionY.Clear();
            enemyData.enemyPositionZ.Clear();

            enemyData.enemyRotationX.Clear();
            enemyData.enemyRotationY.Clear();
            enemyData.enemyRotationZ.Clear();

            enemyData.health.AddRange(new float[numOfEnemies]);
            enemyData.maxHealth.AddRange(new float[numOfEnemies]);
            enemyData.mana.AddRange(new float[numOfEnemies]);
            enemyData.maxMana.AddRange(new float[numOfEnemies]);
            enemyData.doesAIUseMagic.AddRange(new bool[numOfEnemies]);
            enemyData.doesAIPatrol.AddRange(new bool[numOfEnemies]);
            enemyData.patrolWait.AddRange(new float[numOfEnemies]);
            enemyData.patrolRouteInt.AddRange(new int[numOfEnemies]);
            enemyData.patrolPointInt.AddRange(new int[numOfEnemies]);

            enemyData.enemyPositionX.AddRange(new float[numOfEnemies]);
            enemyData.enemyPositionY.AddRange(new float[numOfEnemies]);
            enemyData.enemyPositionZ.AddRange(new float[numOfEnemies]);

            enemyData.enemyRotationX.AddRange(new float[numOfEnemies]);
            enemyData.enemyRotationY.AddRange(new float[numOfEnemies]);
            enemyData.enemyRotationZ.AddRange(new float[numOfEnemies]);

            for (int i = 0; i < numOfEnemies; i++)
            {

                NormEnemyBehavior enemyAIScriptLocal = enemyObjectsListLocal[i].GetComponent<NormEnemyBehavior>();

                enemyData.health[i] = enemyAIScriptLocal.health;
                enemyData.maxHealth[i] = enemyAIScriptLocal.maxHealth;
                enemyData.mana[i] = enemyAIScriptLocal.mana;
                enemyData.maxMana[i] = enemyAIScriptLocal.maxMana;
                enemyData.doesAIUseMagic[i] = enemyAIScriptLocal.doesAIUseMagic;
                enemyData.doesAIPatrol[i] = enemyAIScriptLocal.doesAIPatrol;
                enemyData.patrolWait[i] = enemyAIScriptLocal.patrolWaitFloat;
                enemyData.patrolRouteInt[i] = enemyAIScriptLocal.patrolRouteInt;
                enemyData.patrolPointInt[i] = enemyAIScriptLocal.patrolPointInt;

                enemyData.enemyPositionX[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localPosition.x, 3);
                enemyData.enemyPositionY[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localPosition.y, 3);
                enemyData.enemyPositionZ[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localPosition.z, 3);

                enemyData.enemyRotationX[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localEulerAngles.x, 3);
                enemyData.enemyRotationY[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localEulerAngles.y, 3);
                enemyData.enemyRotationZ[i] = MathService.RoundFloatToDecimalPoint(enemyObjectsListLocal[i].transform.localEulerAngles.z, 3);

            }

            if (DataService.SaveData(enemyDataFileName + slotDataClass.fileName[slotIndex], enemyData, false) == true)
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

            return true;

        }

    }

    public bool SaveSettingsDataBool()
    {

        SettingsManager settingsManagerScript;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            settingsManagerScript = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<SettingsManager>();

        }
        else
        {

            settingsManagerScript = GameObject.FindGameObjectWithTag("UIManager").GetComponent<SettingsManager>();

        }

        SettingsData settingsDataClass = new SettingsData();
        Camera camLocal = Camera.main;
        PlayerCamScript playerCamScriptLocal = camLocal.GetComponent<PlayerCamScript>();

        settingsDataClass.openSettingsTab = settingsManagerScript.openCategory;
        settingsDataClass.xMouseSensitivity = playerCamScriptLocal.xMouseSensitivity;
        settingsDataClass.yMouseSensitivity = playerCamScriptLocal.yMouseSensitivity;

        if (DataService.SaveData(settingsDataFileName, settingsDataClass, false))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

}