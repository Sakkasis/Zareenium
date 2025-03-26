public interface IDataClasses
{

    public SaveManagerData SaveManagerDataClass();

    public SlotData SlotDataClass();

    public PlayerData PlayerDataClass(int slotIndex);

    public EnemyData EnemyDataClass(int slotIndex);

    public SettingsData SettingsDataClass();

    public AudioData AudioDataClass();

    public SubTitleTextPrompts SubTitleTextPromptsClass();

    public GridData GridDataClass();

    public string SlotFileName(int slotIndex);

}