using System;

[Serializable]
public class SaveManagerData
{

    public bool playersFirstSession;
    public int currentlySelectedSaveSlot;
    public bool loadDataOnSceneLoad;
    public bool savedRecently;
    public bool quitWithoutSavingLastTime;

}