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

    public List<string> FormatAllData()
    {

        ConfigData data = configLogic.LoadConfigData();
        List<string> promptStrings = new List<string>();

        promptStrings.Clear();
        promptStrings.Add("Loaded Config Data:");
        promptStrings.Add("    Player:");
        promptStrings.Add("        Physics:");
        promptStrings.AddRange(formatData.PlayerPhysicsFormat(data, 3));
        promptStrings.Add("        Stats:");
        promptStrings.AddRange(formatData.PlayerHpManaStatsFormat(data, 3));
        promptStrings.Add("        Attack:");
        promptStrings.AddRange(formatData.PlayerAttackFormat(data, 3));
        promptStrings.Add("        Location:");
        promptStrings.Add("            Player Object:");
        promptStrings.Add("                Position:");
        promptStrings.AddRange(formatData.PlayerPositionFormat(data, 5));
        promptStrings.Add("                Rotation:");
        promptStrings.AddRange(formatData.PlayerRotationFormat(data, 5));
        promptStrings.Add("            Camera Object:");
        promptStrings.Add("                Position:");
        promptStrings.AddRange(formatData.CameraPositionFormat(data, 5));
        promptStrings.Add("                Rotation:");
        promptStrings.AddRange(formatData.CameraRotationFormat(data, 5));
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
            promptStrings.AddRange(formatData.EnemyPositionFormat(data, 5, i));
            promptStrings.Add("                Rotation:");
            promptStrings.AddRange(formatData.EnemyRotationFormat(data, 5, i));
        }

        return promptStrings;

    }

    public List<string> SetDataByCommand(string command, string input)
    {

        ConfigData data = configLogic.LoadConfigData();
        string dataInput = input;
        char c = char.MinValue;
        for (int i = 0; i < dataInput.Length; i++)
        {
            if (i == dataInput.Length - 1)
            {
                c = dataInput[i];
            }
        }
        dataInput = dataInput.Trim(c);

        if (command.Contains("Player", StringComparison.Ordinal))
        {

            if (command.Contains("Physics", StringComparison.Ordinal))
            {

                if (command.Contains("Walk", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pWalkSpeed = result;
                    return FormatAllDataAfterCommand(data);

                }
                else if (command.Contains("Run", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pRunSpeed = result;
                    return FormatAllDataAfterCommand(data);

                }
                else if (command.Contains("Jump", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pJumpHeight = result;
                    return FormatAllDataAfterCommand(data);

                }
                else if (command.Contains("Gravity", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pGravity = result;
                    return FormatAllDataAfterCommand(data);

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else if (command.Contains("Stats", StringComparison.Ordinal))
            {

                if (command.Contains("Max", StringComparison.Ordinal))
                {

                    if (command.Contains("Health", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        Debug.Log(result + " | " + input + " | " + dataInput + " | " + command + " | " + result.GetType() + " == " + input.GetType());
                        data.pMaxHealth = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.pMaxMana = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Regen", StringComparison.Ordinal))
                {

                    if (command.Contains("Amount", StringComparison.Ordinal))
                    {

                        if (command.Contains("Health", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.pHpRegenAmount = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Mana", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.pManaRegenAmount = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else
                        {

                            List<string> proxy = new List<string>();
                            proxy.Add("Unknown command detected! Please try again.");
                            return proxy;

                        }

                    }
                    else if (command.Contains("Cooldown", StringComparison.Ordinal))
                    {

                        if (command.Contains("Health", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.pHpRegenCooldown = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Mana", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.pManaRegenCooldown = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else
                        {

                            List<string> proxy = new List<string>();
                            proxy.Add("Unknown command detected! Please try again.");
                            return proxy;

                        }

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else
                {

                    if (command.Contains("Health", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.pHealth = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.pMana = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }

            }
            else if (command.Contains("Attack", StringComparison.Ordinal))
            {

                if (command.Contains("Crit", StringComparison.Ordinal))
                {

                    if (command.Contains("Rate", StringComparison.Ordinal))
                    {

                        int.TryParse(dataInput, out int result);
                        data.pCritRate = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Damage", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.pCritDamage = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Amount", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pDamageAmount = result;
                    return FormatAllDataAfterCommand(data);

                }
                else if (command.Contains("Cooldown", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pAttackCooldown = result;
                    return FormatAllDataAfterCommand(data);

                }
                else if (command.Contains("Cost", StringComparison.Ordinal))
                {

                    float.TryParse(dataInput, out float result);
                    data.pAttackManaCost = result;
                    return FormatAllDataAfterCommand(data);

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else if (command.Contains("Location", StringComparison.Ordinal))
            {

                if (command.Contains("Camera", StringComparison.Ordinal))
                {

                    if (command.Contains("Position", StringComparison.Ordinal))
                    {

                        if (command.Contains("X", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camPositionX = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Y", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camPositionY = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Z", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camPositionZ = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else
                        {

                            List<string> proxy = new List<string>();
                            proxy.Add("Unknown command detected! Please try again.");
                            return proxy;

                        }

                    }
                    else if (command.Contains("Rotation", StringComparison.Ordinal))
                    {

                        if (command.Contains("X", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camRotationX = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Y", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camRotationY = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else if (command.Contains("Z", StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.camRotationZ = result;
                            return FormatAllDataAfterCommand(data);

                        }
                        else
                        {

                            List<string> proxy = new List<string>();
                            proxy.Add("Unknown command detected! Please try again.");
                            return proxy;

                        }

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Position", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerPositionX = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerPositionY = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerPositionZ = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Rotation", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerRotationX = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerRotationY = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        float.TryParse(dataInput, out float result);
                        data.playerRotationZ = result;
                        return FormatAllDataAfterCommand(data);

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else
            {

                List<string> proxy = new List<string>();
                proxy.Add("Unknown command detected! Please try again.");
                return proxy;

            }

        }
        else if (command.Contains("Enemy", StringComparison.Ordinal))
        {

            if (command.Contains("Stats", StringComparison.Ordinal))
            {

                if (command.Contains("Max", StringComparison.Ordinal))
                {

                    if (command.Contains("Health", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.eMaxHealth.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.eMaxHealth[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.eMaxMana.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.eMaxMana[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Health", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eHealth.Count; i++)
                    {

                        if (command.Contains((char)i, StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.eHealth[i] = result;
                            return FormatAllDataAfterCommand(data);

                        }

                    }

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }
                else if (command.Contains("Mana", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eMana.Count; i++)
                    {

                        if (command.Contains((char)i, StringComparison.Ordinal))
                        {

                            float.TryParse(dataInput, out float result);
                            data.eMana[i] = result;
                            return FormatAllDataAfterCommand(data);

                        }

                    }

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else if (command.Contains("Logic", StringComparison.Ordinal))
            {

                if (command.Contains("Patrol", StringComparison.Ordinal))
                {

                    if (command.Contains("Does", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.eDoesAIPatrol.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                bool result;
                                if (dataInput.Contains("T"))
                                {
                                    result = true;
                                }
                                else
                                {
                                    result = false;
                                }
                                data.eDoesAIPatrol[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Route", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolRouteInt.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                int.TryParse(dataInput, out int result);
                                data.ePatrolRouteInt[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Point", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolPointInt.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                int.TryParse(dataInput, out int result);
                                data.ePatrolPointInt[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Wait", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolWait.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.ePatrolWait[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Magic", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eDoesAIUseMagic.Count; i++)
                    {

                        if (command.Contains((char)i, StringComparison.Ordinal))
                        {

                            bool result;
                            if (dataInput.Contains("T"))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                            data.eDoesAIUseMagic[i] = result;
                            return FormatAllDataAfterCommand(data);

                        }

                    }

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else if (command.Contains("Location", StringComparison.Ordinal))
            {

                if (command.Contains("Position", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyPositionX.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyPositionX[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyPositionY.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyPositionY[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyPositionZ.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyPositionZ[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else if (command.Contains("Rotation", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationX.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyRotationX[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationY.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyRotationY[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationZ.Count; i++)
                        {

                            if (command.Contains((char)i, StringComparison.Ordinal))
                            {

                                float.TryParse(dataInput, out float result);
                                data.enemyRotationZ[i] = result;
                                return FormatAllDataAfterCommand(data);

                            }

                        }

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }
                    else
                    {

                        List<string> proxy = new List<string>();
                        proxy.Add("Unknown command detected! Please try again.");
                        return proxy;

                    }

                }
                else
                {

                    List<string> proxy = new List<string>();
                    proxy.Add("Unknown command detected! Please try again.");
                    return proxy;

                }

            }
            else
            {

                List<string> proxy = new List<string>();
                proxy.Add("Unknown command detected! Please try again.");
                return proxy;

            }

        }
        else
        {

            List<string> proxy = new List<string>();
            proxy.Add("Unknown command detected! Please try again.");
            return proxy;

        }

    }

    private List<string> FormatAllDataAfterCommand(ConfigData data)
    {

        List<string> promptStrings = new List<string>();

        promptStrings.Clear();
        promptStrings.Add("Loaded Config Data:");
        promptStrings.Add("    Player:");
        promptStrings.Add("        Physics:");
        promptStrings.AddRange(formatData.PlayerPhysicsFormat(data, 3));
        promptStrings.Add("        Stats:");
        promptStrings.AddRange(formatData.PlayerHpManaStatsFormat(data, 3));
        promptStrings.Add("        Attack:");
        promptStrings.AddRange(formatData.PlayerAttackFormat(data, 3));
        promptStrings.Add("        Location:");
        promptStrings.Add("            Player Object:");
        promptStrings.Add("                Position:");
        promptStrings.AddRange(formatData.PlayerPositionFormat(data, 5));
        promptStrings.Add("                Rotation:");
        promptStrings.AddRange(formatData.PlayerRotationFormat(data, 5));
        promptStrings.Add("            Camera Object:");
        promptStrings.Add("                Position:");
        promptStrings.AddRange(formatData.CameraPositionFormat(data, 5));
        promptStrings.Add("                Rotation:");
        promptStrings.AddRange(formatData.CameraRotationFormat(data, 5));
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
            promptStrings.AddRange(formatData.EnemyPositionFormat(data, 5, i));
            promptStrings.Add("                Rotation:");
            promptStrings.AddRange(formatData.EnemyRotationFormat(data, 5, i));
        }

        configLogic.SaveConfigData(data);
        return promptStrings;

    }

    private string TrimDataInput(string input)
    {

        char c = char.MinValue;
        for (int i = 0; i < input.Length; i++)
        {
            if (i == input.Length - 1)
            {
                c = input[i];
            }
        }
        string dataInput = input.Trim(c);

        CultureInfo culture = CultureInfo.CurrentCulture;

        return dataInput;

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
            new CustomEnumArray("Health" + i, data.eHealth[i]),
            new CustomEnumArray("Mana" + i, data.eMana[i]),
            new CustomEnumArray("Max Health" + i, data.eMaxHealth[i]),
            new CustomEnumArray("Max Mana" + i, data.eMaxMana[i])
        };

        return eStats;

    }

    CustomEnumArray[] EnemyLogic(ConfigData data, int i)
    {

        CustomEnumArray[] eLogic = new CustomEnumArray[5]
        {
            new CustomEnumArray("Does AI Use Magic" + i, data.eDoesAIUseMagic[i]),
            new CustomEnumArray("Does AI Patrol" + i, data.eDoesAIPatrol[i]),
            new CustomEnumArray("Patrol Route" + i, data.ePatrolRouteInt[i]),
            new CustomEnumArray("Patrol Point" + i, data.ePatrolPointInt[i]),
            new CustomEnumArray("Patrol Wait" + i, data.ePatrolWait[i])
        };

        return eLogic;

    }

    CustomEnumArray[] EnemyPosition(ConfigData data, int i)
    {

        CustomEnumArray[] ePosition = new CustomEnumArray[3]
        {
            new CustomEnumArray("X" + i, data.enemyPositionX[i]),
            new CustomEnumArray("Y" + i, data.enemyPositionY[i]),
            new CustomEnumArray("Z" + i, data.enemyPositionZ[i])
        };

        return ePosition;

    }

    CustomEnumArray[] EnemyRotation(ConfigData data, int i)
    {

        CustomEnumArray[] eRotation = new CustomEnumArray[3]
        {
            new CustomEnumArray("X" + i, data.enemyRotationX[i]),
            new CustomEnumArray("Y" + i, data.enemyRotationY[i]),
            new CustomEnumArray("Z" + i, data.enemyRotationZ[i])
        };

        return eRotation;

    }

}
