using System;
using System.Collections.Generic;

public interface IConfigService
{

    public List<string> FormatAllData();

    public List<string> SetDataByCommand(string command, string input, PlayerManager pScript, PlayerCamScript cScript, List<NormEnemyBehavior> aiScript, ConfigManager configScript);

}