public class DataClasses : IDataClasses
{

    IDataService DataService = new JsonDataService();

    const string slotIFileName = "_slot-I";
    const string slotIIFileName = "_slot-II";
    const string slotIIIFileName = "_slot-III";
    const string slotIVFileName = "_slot-IV";
    const string slotVFileName = "_slot-V";

    const string saveManagerDataFileName = "SaveManagerData";
    const string slotDataFileName = "SlotData";
    const string playerDataFileName = "PlayerData";
    const string enemyDataFileName = "EnemyData";
    const string settingsDataFileName = "SettingsData";
    const string audioDataFileName = "AudioSettingsData";
    const string subTitleTextPromptsFileName = "TextPrompts";
    const string gridFileName = "GridData";

    public SaveManagerData SaveManagerDataClass()
    {

        SaveManagerData saveManagerData = new SaveManagerData();
        SaveManagerData data = DataService.LoadData<SaveManagerData>(saveManagerDataFileName, true);

        saveManagerData.playersFirstSession = data.playersFirstSession;
        saveManagerData.currentlySelectedSaveSlot = data.currentlySelectedSaveSlot;
        saveManagerData.loadDataOnSceneLoad = data.loadDataOnSceneLoad;
        saveManagerData.savedRecently = data.savedRecently;
        saveManagerData.quitWithoutSavingLastTime = data.quitWithoutSavingLastTime;

        return saveManagerData;

    }

    public SlotData SlotDataClass()
    {

        SlotData slotData = new SlotData();

        slotData.fileName.Clear();
        slotData.hasPlayerUsedSlotBool.Clear();
        slotData.playersFirstLoad.Clear();
        slotData.savedSceneInt.Clear();
        slotData.timeOfLastSave.Clear();
        slotData.numOfEnemiesInScene.Clear();

        slotData.fileName.AddRange(new string[5]);
        slotData.hasPlayerUsedSlotBool.AddRange(new bool[5]);
        slotData.playersFirstLoad.AddRange(new bool[5]);
        slotData.savedSceneInt.AddRange(new int[5]);
        slotData.timeOfLastSave.AddRange(new string[5]);
        slotData.numOfEnemiesInScene.AddRange(new int[5]);

        SlotData data = DataService.LoadData<SlotData>(slotDataFileName, true);

        for (int i = 0; i < 5; i++)
        {

            slotData.fileName[i] = data.fileName[i];
            slotData.hasPlayerUsedSlotBool[i] = data.hasPlayerUsedSlotBool[i];
            slotData.playersFirstLoad[i] = data.playersFirstLoad[i];
            slotData.savedSceneInt[i] = data.savedSceneInt[i];
            slotData.timeOfLastSave[i] = data.timeOfLastSave[i];
            slotData.numOfEnemiesInScene[i] = data.numOfEnemiesInScene[i];

        }

        return slotData;

    }

    public PlayerData PlayerDataClass(int slotIndex)
    {

        PlayerData playerData = new PlayerData();
        PlayerData data = DataService.LoadData<PlayerData>(playerDataFileName + SlotFileName(slotIndex + 1), false);

        playerData.walkSpeed = data.walkSpeed;
        playerData.runSpeed = data.runSpeed;
        playerData.jumpHeight = data.jumpHeight;
        playerData.gravity = data.gravity;
        playerData.health = data.health;
        playerData.mana = data.mana;
        playerData.maxHealth = data.maxHealth;
        playerData.maxMana = data.maxMana;
        playerData.hpRegenAmount = data.hpRegenAmount;
        playerData.manaRegenAmount = data.manaRegenAmount;
        playerData.hpRegenCooldown = data.hpRegenCooldown;
        playerData.manaRegenCooldown = data.manaRegenCooldown;
        playerData.damageAmount = data.damageAmount;
        playerData.critRate = data.critRate;
        playerData.critDamage = data.critDamage;
        playerData.attackCooldown = data.attackCooldown;
        playerData.attackManaCost = data.attackManaCost;

        playerData.playerPositionX = data.playerPositionX;
        playerData.playerPositionY = data.playerPositionY;
        playerData.playerPositionZ = data.playerPositionZ;

        playerData.playerRotationX = data.playerRotationX;
        playerData.playerRotationY = data.playerRotationY;
        playerData.playerRotationZ = data.playerRotationZ;

        playerData.camPositionX = data.camPositionX;
        playerData.camPositionY = data.camPositionY;
        playerData.camPositionZ = data.camPositionZ;

        playerData.camRotationX = data.camRotationX;
        playerData.camRotationY = data.camRotationY;
        playerData.camRotationZ = data.camRotationZ;

        return playerData;

    }

    public EnemyData EnemyDataClass(int slotIndex)
    {

        EnemyData enemyData = new EnemyData();
        EnemyData data = DataService.LoadData<EnemyData>(enemyDataFileName + SlotFileName(slotIndex + 1), false);
        int numOfEnemies = data.health.Count;

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

            enemyData.health[i] = data.health[i];
            enemyData.maxHealth[i] = data.maxHealth[i];
            enemyData.mana[i] = data.mana[i];
            enemyData.maxMana[i] = data.maxMana[i];
            enemyData.doesAIUseMagic[i] = data.doesAIUseMagic[i];
            enemyData.doesAIPatrol[i] = data.doesAIPatrol[i];
            enemyData.patrolWait[i] = data.patrolWait[i];
            enemyData.patrolRouteInt[i] = data.patrolRouteInt[i];
            enemyData.patrolPointInt[i] = data.patrolRouteInt[i];

            enemyData.enemyPositionX[i] = data.enemyPositionX[i];
            enemyData.enemyPositionY[i] = data.enemyPositionY[i];
            enemyData.enemyPositionZ[i] = data.enemyPositionZ[i];

            enemyData.enemyRotationX[i] = data.enemyRotationX[i];
            enemyData.enemyRotationY[i] = data.enemyRotationY[i];
            enemyData.enemyRotationZ[i] = data.enemyRotationZ[i];

        }

        return enemyData;

    }

    public SettingsData SettingsDataClass()
    {

        SettingsData settingsData = new SettingsData();
        SettingsData data = DataService.LoadData<SettingsData>(settingsDataFileName, false);

        settingsData.openSettingsTab = data.openSettingsTab;
        settingsData.xMouseSensitivity = data.xMouseSensitivity;
        settingsData.yMouseSensitivity = data.yMouseSensitivity;

        return settingsData;

    }

    public AudioData AudioDataClass()
    {

        AudioData audioData = new AudioData();
        AudioData data = DataService.LoadData<AudioData>(audioDataFileName, false);

        audioData.masterVolume = data.masterVolume;
        audioData.musicVolume = data.musicVolume;
        audioData.effectsVolume = data.effectsVolume;

        return audioData;

    }

    public SubTitleTextPrompts SubTitleTextPromptsClass()
    {

        SubTitleTextPrompts textPromptsData = new SubTitleTextPrompts();
        SubTitleTextPrompts data = DataService.LoadData<SubTitleTextPrompts>(subTitleTextPromptsFileName, false);

        textPromptsData.textPrompts.Clear();
        textPromptsData.textPrompts.AddRange(new string[data.textPrompts.Count]);

        for (int i = 0; i < data.textPrompts.Count; i++)
        {

            textPromptsData.textPrompts[i] = data.textPrompts[i];

        }

        return textPromptsData;

    }

    public GridData GridDataClass()
    {

        GridData gridData = new GridData();
        GridData data = DataService.LoadData<GridData>(gridFileName, true);

        gridData.numOfRays = data.numOfRays;
        gridData.gridX.Clear();
        gridData.gridY.Clear();

        for (int i = 0; i < data.gridX.Count; i++)
        {

            gridData.gridX.Add(data.gridX[i]);

        }
        for (int i = 0; i < data.gridY.Count; i++)
        {

            gridData.gridY.Add(data.gridY[i]);

        }

        return data;

    }

    public string SlotFileName(int slotIndex)
    {

        if (slotIndex == 1)
        {

            return slotIFileName;

        }
        else if (slotIndex == 2)
        {

            return slotIIFileName;

        }
        else if (slotIndex == 3)
        {

            return slotIIIFileName;

        }
        else if (slotIndex == 4)
        {

            return slotIVFileName;

        }
        else if (slotIndex == 5)
        {

            return slotVFileName;

        }
        else
        {

            return null;

        }

    }

}