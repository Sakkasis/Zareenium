using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigService
{

    const string fileName = "ConfigData";
    IDataService DataService = new JsonDataService();

    public string ConfigDataFormatted()
    {

        string configPath = Application.persistentDataPath + "/" + fileName + ".json";
        string playerPath = Application.persistentDataPath + "/" + fileName + ".json";
        string enemyPath = Application.persistentDataPath + "/" + fileName + ".json";

        if (File.Exists(configPath))
        {

            ConfigData data = DataService.LoadData<ConfigData>(configPath, true);
            string formattedData = "Config Datafile\n";



            return formattedData;

        }
        else
        {

            return null;

        }

    }

}

public class ConfigVariable
{

    public ConfigVariable(string variableName, object variable)
    {

        this.variableName = variableName;
        this.variable = variable;

    }

    public string variableName;
    public object variable;

}

public class ConfigVariableList : IEnumerable
{

    private ConfigVariable[] variables;

    public ConfigVariableList(ConfigVariable[] pArray)
    {

        variables = new ConfigVariable[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {

            variables[i] = pArray[i];

        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

        return (IEnumerator)GetEnumerator();

    }

    public CustomEnumerator GetEnumerator()
    {

        return new CustomEnumerator(variables);

    }

}

public class CustomEnumerator : IEnumerator
{

    public ConfigVariable[] variables;
    public int position = -1;

    public CustomEnumerator(ConfigVariable[] list)
    {

        variables = list;

    }

    public bool MoveNext()
    {

        position++;
        return (position < variables.Length);

    }

    public void Reset()
    {

        position = -1;

    }

    object IEnumerator.Current
    {

        get
        {

            return Current;

        }

    }

    public ConfigVariable Current
    {

        get
        {

            try
            {

                return variables[position];

            }
            catch (IndexOutOfRangeException)
            {

                throw new InvalidOperationException();

            }

        }

    }

}
