using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigService : IConfigService
{

    ConfigLogic configLogic = new ConfigLogic();
    FormatConfigData formatData = new FormatConfigData();

    public string FormatData(string input)
    {

        ConfigData data = configLogic.LoadConfigData();
        string formattedDataString;

        if (input.Equals("All", StringComparison.Ordinal))
        {

            formattedDataString = FormatAllData(data);

        }
        else
        {

            formattedDataString = "!ERROR! Unrecognized command, please try again.";

        }

        return formattedDataString;

    }

    string FormatAllData(ConfigData data)
    {

        string dataString;

        string pPhysics = FormatArray(formatData.PlayerPhysics(data));
        string pHpManaStats = FormatArray(formatData.PlayerHpManaStats(data));
        string pAttack = FormatArray(formatData.PlayerAttack(data));
        string pPosition = FormatArray(formatData.PlayerPosition(data));
        string pRotation = FormatArray(formatData.PlayerRotation(data));
        string cPosition = FormatArray(formatData.CameraPosition(data));
        string cRotation = FormatArray(formatData.CameraRotation(data));

        List<string> eStats = new List<string>();
        List<string> eLogic = new List<string>();
        List<string> ePosition = new List<string>();
        List<string> eRotation = new List<string>();

        for (int i = 0; i < data.eHealth.Count; i++)
        {

            eStats.Add(FormatArray(formatData.EnemyStats(data, i)));
            eLogic.Add(FormatArray(formatData.EnemyLogic(data, i)));
            ePosition.Add(FormatArray(formatData.EnemyPosition(data, i)));
            eRotation.Add(FormatArray(formatData.EnemyRotation(data, i)));

        }

        dataString = "Loaded Config Data\n" +
            "   Player:\n" +
            "       Physics:\n" +
            "           ";



        return dataString;

    }

    string FormatArray(CustomEnumArray[] customArray)
    {

        string dataString = "";

        foreach (CustomEnumArray array in customArray)
        {

            dataString = dataString + array.variableName + ": " + array.variable + "\n";

        }

        return dataString;

    }

}

public class FormatConfigData
{

    public CustomEnumArray[] PlayerPhysics(ConfigData data)
    {

        CustomEnumArray[] pPhysics = new CustomEnumArray[4]
        {
            new CustomEnumArray("Walk Speed", data.pWalkSpeed),
            new CustomEnumArray("Run Speed", data.pRunSpeed),
            new CustomEnumArray("Jump Height", data.pJumpHeight),
            new CustomEnumArray("Gravity", data.pGravity)
        };

        return pPhysics;

    }

    public CustomEnumArray[] PlayerHpManaStats(ConfigData data)
    {

        CustomEnumArray[] pHpManaStats = new CustomEnumArray[8]
        {
            new CustomEnumArray("Health", data.pHealth),
            new CustomEnumArray("Mana", data.pMana),
            new CustomEnumArray("Max Health", data.pMaxHealth),
            new CustomEnumArray("Max Mana", data.pMaxMana),
            new CustomEnumArray("Health Regen Amount", data.pHpRegenAmount),
            new CustomEnumArray("Mana Regen Amount", data.pManaRegenAmount),
            new CustomEnumArray("Health Regen Cooldown", data.pHpRegenCooldown),
            new CustomEnumArray("Mana Regen Cooldown", data.pManaRegenCooldown)
        };

        return pHpManaStats;

    }

    public CustomEnumArray[] PlayerAttack(ConfigData data)
    {

        CustomEnumArray[] pAttack = new CustomEnumArray[5]
        {
            new CustomEnumArray("Damage Amount", data.pDamageAmount),
            new CustomEnumArray("Crit Rate", data.pCritRate),
            new CustomEnumArray("Crit Damage", data.pCritDamage),
            new CustomEnumArray("Cooldown", data.pAttackCooldown),
            new CustomEnumArray("Mana Cost", data.pAttackManaCost)
        };

        return pAttack;

    }

    public CustomEnumArray[] PlayerPosition(ConfigData data)
    {

        CustomEnumArray[] pPosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.playerPositionX),
            new CustomEnumArray("Y", data.playerPositionY),
            new CustomEnumArray("Z", data.playerPositionZ)
        };

        return pPosition;

    }

    public CustomEnumArray[] PlayerRotation(ConfigData data)
    {

        CustomEnumArray[] pRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.playerRotationX),
            new CustomEnumArray("Y", data.playerRotationY),
            new CustomEnumArray("Z", data.playerRotationZ)
        };

        return pRotation;

    }

    public CustomEnumArray[] CameraPosition(ConfigData data)
    {

        CustomEnumArray[] cPosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.camPositionX),
            new CustomEnumArray("Y", data.camPositionY),
            new CustomEnumArray("Z", data.camPositionZ)
        };

        return cPosition;

    }

    public CustomEnumArray[] CameraRotation(ConfigData data)
    {

        CustomEnumArray[] cRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.camRotationX),
            new CustomEnumArray("Y", data.camRotationX),
            new CustomEnumArray("Z", data.camRotationX)
        };

        return cRotation;

    }

    public CustomEnumArray[] EnemyStats(ConfigData data, int i)
    {

        CustomEnumArray[] eStats = new CustomEnumArray[4]
        {
            new CustomEnumArray("Health", data.eHealth[i]),
            new CustomEnumArray("Mana", data.eMana[i]),
            new CustomEnumArray("Max Health", data.eMaxHealth[i]),
            new CustomEnumArray("Max Mana", data.eMaxMana[i])
        };

        return eStats;

    }

    public CustomEnumArray[] EnemyLogic(ConfigData data, int i)
    {

        CustomEnumArray[] eLogic = new CustomEnumArray[5]
        {
            new CustomEnumArray("Does AI Use Magic", data.eDoesAIUseMagic[i]),
            new CustomEnumArray("Does AI Patrol", data.eDoesAIPatrol[i]),
            new CustomEnumArray("Patrol Route", data.ePatrolRouteInt[i]),
            new CustomEnumArray("Patrol Point", data.ePatrolPointInt[i]),
            new CustomEnumArray("Patrol Wait", data.ePatrolWait[i])
        };

        return eLogic;

    }

    public CustomEnumArray[] EnemyPosition(ConfigData data, int i)
    {

        CustomEnumArray[] ePosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.enemyPositionX[i]),
            new CustomEnumArray("Y", data.enemyPositionY[i]),
            new CustomEnumArray("Z", data.enemyPositionZ[i])
        };

        return ePosition;

    }

    public CustomEnumArray[] EnemyRotation(ConfigData data, int i)
    {

        CustomEnumArray[] eRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.enemyRotationX[i]),
            new CustomEnumArray("Y", data.enemyRotationY[i]),
            new CustomEnumArray("Z", data.enemyRotationZ[i])
        };

        return eRotation;

    }

}
