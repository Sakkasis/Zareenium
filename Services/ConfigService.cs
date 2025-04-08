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

    public List<string> FormatData(string input)
    {

        ConfigData data = configLogic.LoadConfigData();
        List<string> formattedDataStrings = new List<string>();

        if (input.Equals("All", StringComparison.Ordinal))
        {

            formattedDataStrings = FormatAllData(data);

        }
        else
        {

            formattedDataStrings.Clear();
            formattedDataStrings.Add("!ERROR! Unrecognized command, please try again.");

        }

        return formattedDataStrings;

    }

    List<string> FormatAllData(ConfigData data)
    {

        List<string> promptStrings = new List<string>();

        promptStrings.Clear();
        promptStrings.Add("Loaded Config Data:");
        promptStrings.Add("    Player:");
        promptStrings.Add("        Physics:");
        promptStrings.AddRange(formatData.PlayerPhysicsFormat(data, 3));
        promptStrings.Add("        HP and Mana:");
        promptStrings.AddRange(formatData.PlayerHpManaStatsFormat(data, 3));
        promptStrings.Add("        Attack:");
        promptStrings.AddRange(formatData.PlayerAttackFormat(data, 3));
        promptStrings.Add("        Locaiton:");
        promptStrings.Add("            Position:");
        promptStrings.AddRange(formatData.PlayerPositionFormat(data, 4));
        promptStrings.Add("            Rotation:");
        promptStrings.AddRange(formatData.PlayerRotationFormat(data, 4));
        promptStrings.Add("    Enemies:");

        for (int i = 0; i < data.eHealth.Count; i++)
        {
            promptStrings.Add("        Enemy" + i + ":");
            promptStrings.Add("            Stats:");
            promptStrings.AddRange(formatData.EnemyStatsFormat(data, 4, i));
            promptStrings.Add("            Logic:");
            promptStrings.AddRange(formatData.EnemyLogicFormat(data, 4, i));
            promptStrings.Add("            Location:");
            promptStrings.Add("                Position:");
            promptStrings.AddRange(formatData.EnemyLogicFormat(data, 5, i));
            promptStrings.Add("                Rotation:");
            promptStrings.AddRange(formatData.EnemyLogicFormat(data, 5, i));
        }

        return promptStrings;

    }

}

public class FormatConfigData
{

    public List<string> PlayerPhysicsFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in PlayerPhysics(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> PlayerHpManaStatsFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in PlayerHpManaStats(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> PlayerAttackFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in PlayerAttack(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> PlayerPositionFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in PlayerPosition(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> PlayerRotationFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in PlayerRotation(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> CameraPositionFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in CameraPosition(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> CameraRotationFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in CameraRotation(data))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> EnemyStatsFormat(ConfigData data, int indents, int index)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in EnemyStats(data, index))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> EnemyLogicFormat(ConfigData data, int indents, int index)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in EnemyLogic(data, index))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> EnemyPositionFormat(ConfigData data, int indents, int index)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in EnemyPosition(data, index))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    public List<string> EnemyRotationFormat(ConfigData data, int indents, int index)
    {

        List<string> promptStrings = new List<string>();

        foreach (CustomEnumArray ceu in EnemyRotation(data, index))
        {
            string prompt = "";
            for (int i = 0; i < indents; i++)
            {
                prompt += "    ";
            }
            prompt += ceu.variableName + ": " + ceu.variable;
            promptStrings.Add(prompt);
        }

        return promptStrings;

    }

    CustomEnumArray[] PlayerPhysics(ConfigData data)
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

    CustomEnumArray[] PlayerHpManaStats(ConfigData data)
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

    CustomEnumArray[] PlayerAttack(ConfigData data)
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

    CustomEnumArray[] PlayerPosition(ConfigData data)
    {

        CustomEnumArray[] pPosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.playerPositionX),
            new CustomEnumArray("Y", data.playerPositionY),
            new CustomEnumArray("Z", data.playerPositionZ)
        };

        return pPosition;

    }

    CustomEnumArray[] PlayerRotation(ConfigData data)
    {

        CustomEnumArray[] pRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.playerRotationX),
            new CustomEnumArray("Y", data.playerRotationY),
            new CustomEnumArray("Z", data.playerRotationZ)
        };

        return pRotation;

    }

    CustomEnumArray[] CameraPosition(ConfigData data)
    {

        CustomEnumArray[] cPosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.camPositionX),
            new CustomEnumArray("Y", data.camPositionY),
            new CustomEnumArray("Z", data.camPositionZ)
        };

        return cPosition;

    }

    CustomEnumArray[] CameraRotation(ConfigData data)
    {

        CustomEnumArray[] cRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.camRotationX),
            new CustomEnumArray("Y", data.camRotationX),
            new CustomEnumArray("Z", data.camRotationX)
        };

        return cRotation;

    }

    CustomEnumArray[] EnemyStats(ConfigData data, int i)
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

    CustomEnumArray[] EnemyLogic(ConfigData data, int i)
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

    CustomEnumArray[] EnemyPosition(ConfigData data, int i)
    {

        CustomEnumArray[] ePosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X", data.enemyPositionX[i]),
            new CustomEnumArray("Y", data.enemyPositionY[i]),
            new CustomEnumArray("Z", data.enemyPositionZ[i])
        };

        return ePosition;

    }

    CustomEnumArray[] EnemyRotation(ConfigData data, int i)
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
