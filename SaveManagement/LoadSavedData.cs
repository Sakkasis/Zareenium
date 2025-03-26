using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavedGameState
{

    IDataClasses DataClasses = new DataClasses();

    [SerializeField] GameObject player;

    public bool LoadPlayerDataBool(GameObject playerPrefab, GameObject camPrefab, int slotIndex)
    {

        PlayerData playerData = DataClasses.PlayerDataClass(slotIndex);
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject playerObject = GameObject.FindGameObjectWithTag("bean");

        Vector3 playerPosition;
        playerPosition.x = playerData.playerPositionX;
        playerPosition.y = playerData.playerPositionY;
        playerPosition.z = playerData.playerPositionZ;

        Quaternion playerRotation = new Quaternion();
        playerRotation.x = playerData.playerRotationX;
        playerRotation.y = playerData.playerRotationY;
        playerRotation.z = playerData.playerRotationZ;

        Vector3 camPosition;
        camPosition.x = playerData.camPositionX;
        camPosition.y = playerData.camPositionY;
        camPosition.z = playerData.camPositionZ;

        Quaternion camRotation = new Quaternion();
        camRotation.x = playerData.camRotationX;
        camRotation.y = playerData.camRotationY;
        camRotation.z = playerData.camRotationZ;

        GameObject.Destroy(cam);
        GameObject.Destroy(playerObject);

        GameObject newPlayerObject = GameObject.Instantiate(playerPrefab, playerPosition, playerRotation);
        GameObject newMainCam = GameObject.Instantiate(camPrefab, camPosition, camRotation);

        newPlayerObject.name = "bean";
        newMainCam.name = "mainCam";

        PlayerManager playerScript = newPlayerObject.GetComponent<PlayerManager>();
        PlayerCamScript playerCamScript = newMainCam.GetComponent<PlayerCamScript>();

        playerScript.walkSpeed = playerData.walkSpeed;
        playerScript.runSpeed = playerData.runSpeed;
        playerScript.jumpHeight = playerData.jumpHeight;
        playerScript.gravity = playerData.gravity;
        playerScript.health = playerData.health;
        playerScript.maxHealth = playerData.maxHealth;
        playerScript.mana = playerData.mana;
        playerScript.maxMana = playerData.maxMana;
        playerScript.hpRegenAmount = playerData.hpRegenAmount;
        playerScript.manaRegenAmount = playerData.manaRegenAmount;
        playerScript.hpRegenCooldown = playerData.hpRegenCooldown;
        playerScript.manaRegenCooldown = playerData.manaRegenCooldown;
        playerScript.damageAmount = playerData.damageAmount;
        playerScript.critRate = playerData.critRate;
        playerScript.critDamage = playerData.critDamage;
        playerScript.attackCooldown = playerData.attackCooldown;
        playerScript.attackManaCost = playerData.attackManaCost;

        return true;

    }

    public bool LoadEnemyDataBool(int slotIndex, GameObject enemyPrefab)
    {

        SlotData slotDataClass = DataClasses.SlotDataClass();

        List<GameObject> enemyObjectsInScene = new List<GameObject>();
        enemyObjectsInScene.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        if (enemyObjectsInScene.Count != 0)
        {

            foreach (GameObject enemyObject in enemyObjectsInScene)
            {

                GameObject.Destroy(enemyObject);

            }

        }

        if (slotDataClass.numOfEnemiesInScene[slotIndex] != 0)
        {

            EnemyData enemyDataClass = DataClasses.EnemyDataClass(slotIndex);

            for (int i = 0; i < slotDataClass.numOfEnemiesInScene[slotIndex]; i++)
            {

                Vector3 newEnemyVector = new Vector3();
                newEnemyVector.x = enemyDataClass.enemyPositionX[i];
                newEnemyVector.y = enemyDataClass.enemyPositionY[i];
                newEnemyVector.z = enemyDataClass.enemyPositionZ[i];

                Quaternion newEnemyQuaternion = new Quaternion();
                newEnemyQuaternion.x = enemyDataClass.enemyRotationX[i];
                newEnemyQuaternion.y = enemyDataClass.enemyRotationY[i];
                newEnemyQuaternion.z = enemyDataClass.enemyRotationZ[i];

                GameObject newEnemyObject = GameObject.Instantiate(enemyPrefab, newEnemyVector, newEnemyQuaternion);
                NormEnemyBehavior newEnemyAIScript = newEnemyObject.GetComponent<NormEnemyBehavior>();

                newEnemyObject.name = "cubean" + i.ToString();

                newEnemyAIScript.health = enemyDataClass.health[i];
                newEnemyAIScript.maxHealth = enemyDataClass.maxHealth[i];
                newEnemyAIScript.mana = enemyDataClass.mana[i];
                newEnemyAIScript.maxMana = enemyDataClass.maxMana[i];
                newEnemyAIScript.doesAIUseMagic = enemyDataClass.doesAIUseMagic[i];
                newEnemyAIScript.doesAIPatrol = enemyDataClass.doesAIPatrol[i];
                newEnemyAIScript.patrolRouteInt = enemyDataClass.patrolRouteInt[i];
                newEnemyAIScript.patrolPointInt = enemyDataClass.patrolPointInt[i];

            }

            return true;

        }
        else
        {

            return true;

        }

    }

    public bool LoadSettingsDataBool()
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

        SettingsData settingsDataClass = DataClasses.SettingsDataClass();

        settingsManagerScript.xSensitivitySlider.value = settingsDataClass.xMouseSensitivity;
        settingsManagerScript.ySensitivitySlider.value = settingsDataClass.yMouseSensitivity;
        settingsManagerScript.ChangeCategory(settingsDataClass.openSettingsTab);

        return true;

    }

}