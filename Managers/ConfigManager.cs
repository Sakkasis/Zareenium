using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{

    [Header("Conifg")]
    public bool menuOpen = false;
    [SerializeField] GameObject configCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject enemyCanvas;

    ConfigLogic logicScript = new ConfigLogic();

    private void Awake()
    {

        logicScript.InitializeConfigDataFile();

    }

    private void Start()
    {



    }

    private void Update()
    {
        


    }

    public void ConfigMenu()
    {

        menuOpen = !menuOpen;
        MenuManager menuScript = gameObject.GetComponent<MenuManager>();

        if (menuOpen)
        {

            configCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            menuScript.settingsOpenBool = false;

        }
        else
        {

            configCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            menuScript.settingsOpenBool = true;

        }

    }

}

[Serializable]
public class ConfigLogic
{

    const bool Config = true;
    const string fileName = "ConfigData";
    IDataService DataService = new JsonDataService();
    IDataClasses DataClasses = new DataClasses();

    public void InitializeConfigDataFile()
    {

        ConfigData data = new ConfigData();
        data.ConfigManager = Config;

        SaveManagerData managerData = DataClasses.SaveManagerDataClass();
        if (DataService.DoesFileExist("PlayerData" + DataClasses.SlotFileName(managerData.currentlySelectedSaveSlot - 1)))
        {

            PlayerData playerData = DataClasses.PlayerDataClass(managerData.currentlySelectedSaveSlot);
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

        }

        DataService.SaveData(fileName, data, true);

    }

}
