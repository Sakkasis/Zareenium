using System.Collections.Generic;

[System.Serializable]
public class SlotData
{

    public List<string> fileName = new List<string>();
    public List<bool> hasPlayerUsedSlotBool = new List<bool>();
    public List<bool> playersFirstLoad = new List<bool>();
    public List<int> savedSceneInt = new List<int>();
    public List<string> timeOfLastSave = new List<string>();
    public List<int> numOfEnemiesInScene = new List<int>();

}
