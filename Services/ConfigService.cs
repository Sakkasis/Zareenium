using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigService : IConfigService
{

    IDataClasses DataClasses = new DataClasses();
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

    public List<string> SetDataByCommand(string command, string input, PlayerManager pScript, PlayerCamScript cScript, List<NormEnemyBehavior> aiScript)
    {

        ConfigData data = configLogic.LoadConfigData();
        SaveManagerData smData = DataClasses.SaveManagerDataClass();
        PlayerData pData = DataClasses.PlayerDataClass(smData.currentlySelectedSaveSlot - 1);
        EnemyData eData = DataClasses.EnemyDataClass(smData.currentlySelectedSaveSlot - 1);
        List<string> proxy = new List<string>();

        if (command.Contains("Player", StringComparison.Ordinal))
        {

            if (command.Contains("Physics", StringComparison.Ordinal))
            {

                if (command.Contains("Walk", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pWalkSpeed = result;
                    pData.walkSpeed = result;

                }
                else if (command.Contains("Run", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pRunSpeed = result;
                    pData.runSpeed = result;

                }
                else if (command.Contains("Jump", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pJumpHeight = result;
                    pData.jumpHeight = result;

                }
                else if (command.Contains("Gravity", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pGravity = result;
                    pData.gravity = result;

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }

            }
            else if (command.Contains("Stats", StringComparison.Ordinal))
            {

                if (command.Contains("Max", StringComparison.Ordinal))
                {

                    if (command.Contains("Health", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.pMaxHealth = result;
                        pData.maxHealth = result;

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.pMaxMana = result;
                        pData.maxMana = result;

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Regen", StringComparison.Ordinal))
                {

                    if (command.Contains("Amount", StringComparison.Ordinal))
                    {

                        if (command.Contains("Health", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.pHpRegenAmount = result;
                            pData.hpRegenAmount = result;

                        }
                        else if (command.Contains("Mana", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.pManaRegenAmount = result;
                            pData.manaRegenAmount = result;

                        }
                        else
                        {

                            proxy.Add("Unknown command detected! Please try again.");
                            Debug.Log("Unknown command detected! Please try again.");

                        }

                    }
                    else if (command.Contains("Cooldown", StringComparison.Ordinal))
                    {

                        if (command.Contains("Health", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.pHpRegenCooldown = result;
                            pData.hpRegenCooldown = result;

                        }
                        else if (command.Contains("Mana", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.pManaRegenCooldown = result;
                            pData.manaRegenCooldown = result;

                        }
                        else
                        {

                            proxy.Add("Unknown command detected! Please try again.");
                            Debug.Log("Unknown command detected! Please try again.");

                        }

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else
                {

                    if (command.Contains("Health", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.pHealth = result;
                        pData.health = result;

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.pMana = result;
                        pData.mana = result;

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }

            }
            else if (command.Contains("Attack", StringComparison.Ordinal))
            {

                if (command.Contains("Crit", StringComparison.Ordinal))
                {

                    if (command.Contains("Rate", StringComparison.Ordinal))
                    {

                        int.TryParse(TrimDataInput(input), out int result);
                        data.pCritRate = result;
                        pData.critRate = result;

                    }
                    else if (command.Contains("Damage", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.pCritDamage = result;
                        pData.critDamage = result;

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Amount", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pDamageAmount = result;
                    pData.damageAmount = result;

                }
                else if (command.Contains("Cooldown", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pAttackCooldown = result;
                    pData.attackCooldown = result;

                }
                else if (command.Contains("Cost", StringComparison.Ordinal))
                {

                    float.TryParse(TrimDataInput(input), out float result);
                    data.pAttackManaCost = result;
                    pData.attackManaCost = result;

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

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

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camPositionX = result;
                            pData.camPositionX = result;

                        }
                        else if (command.Contains("Y", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camPositionY = result;
                            pData.camPositionY = result;

                        }
                        else if (command.Contains("Z", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camPositionZ = result;
                            pData.camPositionZ = result;

                        }
                        else
                        {

                            proxy.Add("Unknown command detected! Please try again.");
                            Debug.Log("Unknown command detected! Please try again.");

                        }

                    }
                    else if (command.Contains("Rotation", StringComparison.Ordinal))
                    {

                        if (command.Contains("X", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camRotationX = result;
                            pData.camRotationX = result;

                        }
                        else if (command.Contains("Y", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camRotationY = result;
                            pData.camRotationY = result;

                        }
                        else if (command.Contains("Z", StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.camRotationZ = result;
                            pData.camRotationZ = result;

                        }
                        else
                        {

                            proxy.Add("Unknown command detected! Please try again.");
                            Debug.Log("Unknown command detected! Please try again.");

                        }

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Position", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerPositionX = result;
                        pData.playerPositionX = result;

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerPositionY = result;
                        pData.playerPositionY = result;

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerPositionZ = result;
                        pData.playerPositionZ = result;

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Rotation", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerRotationX = result;
                        pData.playerRotationX = result;

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerRotationY = result;
                        pData.playerRotationY = result;

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        float.TryParse(TrimDataInput(input), out float result);
                        data.playerRotationZ = result;
                        pData.playerRotationZ = result;

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }

            }
            else
            {

                proxy.Add("Unknown command detected! Please try again.");
                Debug.Log("Unknown command detected! Please try again.");

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

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.eMaxHealth[i] = result;
                                eData.maxHealth[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Mana", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.eMaxMana.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.eMaxMana[i] = result;
                                eData.maxMana[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Health", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eHealth.Count; i++)
                    {

                        if (command.Contains("" + i, StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.eHealth[i] = result;
                            eData.health[i] = result;

                        }

                    }

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }
                else if (command.Contains("Mana", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eMana.Count; i++)
                    {

                        if (command.Contains("" + i, StringComparison.Ordinal))
                        {

                            float.TryParse(TrimDataInput(input), out float result);
                            data.eMana[i] = result;
                            eData.mana[i] = result;

                        }

                    }

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

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

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                bool result;
                                if (input.Contains("T"))
                                {
                                    result = true;
                                }
                                else
                                {
                                    result = false;
                                }
                                data.eDoesAIPatrol[i] = result;
                                eData.doesAIPatrol[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Route", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolRouteInt.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                int.TryParse(TrimDataInput(input), out int result);
                                data.ePatrolRouteInt[i] = result;
                                eData.patrolRouteInt[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Point", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolPointInt.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                int.TryParse(TrimDataInput(input), out int result);
                                data.ePatrolPointInt[i] = result;
                                eData.patrolPointInt[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Wait", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.ePatrolWait.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.ePatrolWait[i] = result;
                                eData.patrolWait[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Magic", StringComparison.Ordinal))
                {

                    for (int i = 0; i < data.eDoesAIUseMagic.Count; i++)
                    {

                        if (command.Contains("" + i, StringComparison.Ordinal))
                        {

                            bool result;
                            if (input.Contains("T"))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                            data.eDoesAIUseMagic[i] = result;
                            eData.doesAIUseMagic[i] = result;

                        }

                    }

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

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

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyPositionX[i] = result;
                                eData.enemyPositionX[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyPositionY.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyPositionY[i] = result;
                                eData.enemyPositionY[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyPositionZ.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyPositionZ[i] = result;
                                eData.enemyPositionZ[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else if (command.Contains("Rotation", StringComparison.Ordinal))
                {

                    if (command.Contains("X", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationX.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyRotationX[i] = result;
                                eData.enemyRotationX[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Y", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationY.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyRotationY[i] = result;
                                eData.enemyRotationY[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else if (command.Contains("Z", StringComparison.Ordinal))
                    {

                        for (int i = 0; i < data.enemyRotationZ.Count; i++)
                        {

                            if (command.Contains("" + i, StringComparison.Ordinal))
                            {

                                float.TryParse(TrimDataInput(input), out float result);
                                data.enemyRotationZ[i] = result;
                                eData.enemyRotationZ[i] = result;

                            }

                        }

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }
                    else
                    {

                        proxy.Add("Unknown command detected! Please try again.");
                        Debug.Log("Unknown command detected! Please try again.");

                    }

                }
                else
                {

                    proxy.Add("Unknown command detected! Please try again.");
                    Debug.Log("Unknown command detected! Please try again.");

                }

            }
            else
            {

                proxy.Add("Unknown command detected! Please try again.");
                Debug.Log("Unknown command detected! Please try again.");

            }

        }
        else
        {

            proxy.AddRange(OtherCommands(command, input));
            return proxy;

        }

        proxy.AddRange(FormatAllDataAfterCommand(data, pData, eData));
        return proxy;

    }

    private List<string> FormatAllDataAfterCommand(ConfigData data, PlayerData pData, EnemyData eData)
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

        configLogic.SaveConfigData(data, pData, eData);
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
        string proxy = "";

        if (input.Contains(',') || input.Contains('.'))
        {

            CultureInfo culture = CultureInfo.CurrentCulture;

            for (int i = 0; i < dataInput.Length; i++)
            {

                if (dataInput.ToCharArray()[i] == '.' || dataInput.ToCharArray()[i] == ',')
                {
                    proxy += culture.NumberFormat.NumberDecimalSeparator;
                }
                else
                {
                    proxy += dataInput.ToCharArray()[i];
                }

            }

        }
        else
        {
            proxy = dataInput;
        }

        return proxy;

    }

    private List<string> OtherCommands(string command, string input)
    {

        List<string> proxy = new List<string>();



        return proxy;

    }

}

public class FormatConfigData
{

    public List<string> PlayerPhysicsFormat(ConfigData data, int indents)
    {

        List<string> promptStrings = new List<string>();

        foreach (ConfigEnumArray ceu in PlayerPhysics(data))
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

        foreach (ConfigEnumArray ceu in PlayerHpManaStats(data))
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

        foreach (ConfigEnumArray ceu in PlayerAttack(data))
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

        foreach (ConfigEnumArray ceu in PlayerPosition(data))
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

        foreach (ConfigEnumArray ceu in PlayerRotation(data))
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

        foreach (ConfigEnumArray ceu in CameraPosition(data))
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

        foreach (ConfigEnumArray ceu in CameraRotation(data))
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

        foreach (ConfigEnumArray ceu in EnemyStats(data, index))
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

        foreach (ConfigEnumArray ceu in EnemyLogic(data, index))
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

        foreach (ConfigEnumArray ceu in EnemyPosition(data, index))
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

        foreach (ConfigEnumArray ceu in EnemyRotation(data, index))
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

    ConfigEnumArray[] PlayerPhysics(ConfigData data)
    {

        ConfigEnumArray[] pPhysics = new ConfigEnumArray[4]
        {
            new ConfigEnumArray("Walk Speed", data.pWalkSpeed),
            new ConfigEnumArray("Run Speed", data.pRunSpeed),
            new ConfigEnumArray("Jump Height", data.pJumpHeight),
            new ConfigEnumArray("Gravity", data.pGravity)
        };

        return pPhysics;

    }

    ConfigEnumArray[] PlayerHpManaStats(ConfigData data)
    {

        ConfigEnumArray[] pHpManaStats = new ConfigEnumArray[8]
        {
            new ConfigEnumArray("Health", data.pHealth),
            new ConfigEnumArray("Mana", data.pMana),
            new ConfigEnumArray("Max Health", data.pMaxHealth),
            new ConfigEnumArray("Max Mana", data.pMaxMana),
            new ConfigEnumArray("Health Regen Amount", data.pHpRegenAmount),
            new ConfigEnumArray("Mana Regen Amount", data.pManaRegenAmount),
            new ConfigEnumArray("Health Regen Cooldown", data.pHpRegenCooldown),
            new ConfigEnumArray("Mana Regen Cooldown", data.pManaRegenCooldown)
        };

        return pHpManaStats;

    }

    ConfigEnumArray[] PlayerAttack(ConfigData data)
    {

        ConfigEnumArray[] pAttack = new ConfigEnumArray[5]
        {
            new ConfigEnumArray("Damage Amount", data.pDamageAmount),
            new ConfigEnumArray("Crit Rate", data.pCritRate),
            new ConfigEnumArray("Crit Damage", data.pCritDamage),
            new ConfigEnumArray("Cooldown", data.pAttackCooldown),
            new ConfigEnumArray("Mana Cost", data.pAttackManaCost)
        };

        return pAttack;

    }

    ConfigEnumArray[] PlayerPosition(ConfigData data)
    {

        ConfigEnumArray[] pPosition = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X", data.playerPositionX),
            new ConfigEnumArray("Y", data.playerPositionY),
            new ConfigEnumArray("Z", data.playerPositionZ)
        };

        return pPosition;

    }

    ConfigEnumArray[] PlayerRotation(ConfigData data)
    {

        ConfigEnumArray[] pRotation = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X", data.playerRotationX),
            new ConfigEnumArray("Y", data.playerRotationY),
            new ConfigEnumArray("Z", data.playerRotationZ)
        };

        return pRotation;

    }

    ConfigEnumArray[] CameraPosition(ConfigData data)
    {

        ConfigEnumArray[] cPosition = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X", data.camPositionX),
            new ConfigEnumArray("Y", data.camPositionY),
            new ConfigEnumArray("Z", data.camPositionZ)
        };

        return cPosition;

    }

    ConfigEnumArray[] CameraRotation(ConfigData data)
    {

        ConfigEnumArray[] cRotation = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X", data.camRotationX),
            new ConfigEnumArray("Y", data.camRotationX),
            new ConfigEnumArray("Z", data.camRotationX)
        };

        return cRotation;

    }

    ConfigEnumArray[] EnemyStats(ConfigData data, int i)
    {

        ConfigEnumArray[] eStats = new ConfigEnumArray[4]
        {
            new ConfigEnumArray("Health" + i, data.eHealth[i]),
            new ConfigEnumArray("Mana" + i, data.eMana[i]),
            new ConfigEnumArray("Max Health" + i, data.eMaxHealth[i]),
            new ConfigEnumArray("Max Mana" + i, data.eMaxMana[i])
        };

        return eStats;

    }

    ConfigEnumArray[] EnemyLogic(ConfigData data, int i)
    {

        ConfigEnumArray[] eLogic = new ConfigEnumArray[5]
        {
            new ConfigEnumArray("Does AI Use Magic" + i, data.eDoesAIUseMagic[i]),
            new ConfigEnumArray("Does AI Patrol" + i, data.eDoesAIPatrol[i]),
            new ConfigEnumArray("Patrol Route" + i, data.ePatrolRouteInt[i]),
            new ConfigEnumArray("Patrol Point" + i, data.ePatrolPointInt[i]),
            new ConfigEnumArray("Patrol Wait" + i, data.ePatrolWait[i])
        };

        return eLogic;

    }

    ConfigEnumArray[] EnemyPosition(ConfigData data, int i)
    {

        ConfigEnumArray[] ePosition = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X" + i, data.enemyPositionX[i]),
            new ConfigEnumArray("Y" + i, data.enemyPositionY[i]),
            new ConfigEnumArray("Z" + i, data.enemyPositionZ[i])
        };

        return ePosition;

    }

    ConfigEnumArray[] EnemyRotation(ConfigData data, int i)
    {

        ConfigEnumArray[] eRotation = new ConfigEnumArray[3]
        {
            new ConfigEnumArray("X" + i, data.enemyRotationX[i]),
            new ConfigEnumArray("Y" + i, data.enemyRotationY[i]),
            new ConfigEnumArray("Z" + i, data.enemyRotationZ[i])
        };

        return eRotation;

    }

    public PlayerEnumArray[] PlayerArray(PlayerData data)
    {

        PlayerEnumArray[] pArray = new PlayerEnumArray[23]
        {
            new PlayerEnumArray(0, data.walkSpeed),
            new PlayerEnumArray(1, data.runSpeed),
            new PlayerEnumArray(2, data.jumpHeight),
            new PlayerEnumArray(3, data.gravity),
            new PlayerEnumArray(4, data.health),
            new PlayerEnumArray(5, data.mana),
            new PlayerEnumArray(6, data.maxHealth),
            new PlayerEnumArray(7, data.maxMana),
            new PlayerEnumArray(8, data.hpRegenAmount),
            new PlayerEnumArray(9, data.manaRegenAmount),
            new PlayerEnumArray(10, data.hpRegenCooldown),
            new PlayerEnumArray(11, data.manaRegenCooldown),
            new PlayerEnumArray(12, data.damageAmount),
            new PlayerEnumArray(13, data.critRate),
            new PlayerEnumArray(14, data.critDamage),
            new PlayerEnumArray(15, data.attackCooldown),
            new PlayerEnumArray(16, data.attackManaCost),
            new PlayerEnumArray(0, data.playerPositionX),
            new PlayerEnumArray(0, data.playerPositionY),
            new PlayerEnumArray(0, data.playerPositionZ),
            new PlayerEnumArray(0, data.playerRotationX),
            new PlayerEnumArray(0, data.playerRotationY),
            new PlayerEnumArray(0, data.playerRotationZ)
        };

        return pArray;

    }

    public EnemyEnumArray[] EnemyArray(EnemyData data, int index)
    {

        EnemyEnumArray[] eArray = new EnemyEnumArray[15]
        {
            new EnemyEnumArray(index, 0, data.health),
            new EnemyEnumArray(index, 1, data.maxHealth),
            new EnemyEnumArray(index, 2, data.mana),
            new EnemyEnumArray(index, 3, data.maxMana),
            new EnemyEnumArray(index, 4, data.doesAIUseMagic),
            new EnemyEnumArray(index, 5, data.doesAIPatrol),
            new EnemyEnumArray(index, 6, data.patrolRouteInt),
            new EnemyEnumArray(index, 7, data.patrolPointInt),
            new EnemyEnumArray(index, 8, data.patrolWait),
            new EnemyEnumArray(index, 0, data.enemyPositionX),
            new EnemyEnumArray(index, 0, data.enemyPositionY),
            new EnemyEnumArray(index, 0, data.enemyPositionZ),
            new EnemyEnumArray(index, 0, data.enemyRotationX),
            new EnemyEnumArray(index, 0, data.enemyRotationY),
            new EnemyEnumArray(index, 0, data.enemyRotationZ)
        };

        return eArray;

    }

}
