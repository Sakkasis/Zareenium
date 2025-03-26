using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class RayGrid
{

    const int numOfHLines = 2;
    const int numOfVLines = 2;

    const string fileName = "GridData";
    IDataService DataService = new JsonDataService();

    public void CreateGrid()
    {

        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json") == false)
        {

            GridData gridData = new GridData();
            gridData.numOfRays = (numOfHLines * 2) * (numOfVLines * 2);
            gridData.gridX = gridXList();
            gridData.gridY = gridYList();
            DataService.SaveData(fileName, gridData, true);

        }

    }

    private List<float> gridXList()
    {

        List<float> proxy = new List<float>();

        for (int i = -numOfHLines; i < numOfHLines + 1; i++)
        {

            float x = i;
            proxy.Add(x * 0.5f);

        }

        return proxy;

    }

    private List<float> gridYList()
    {

        List<float> proxy = new List<float>();

        for (int i = -numOfVLines; i < numOfVLines + 1; i++)
        {

            float y = i;
            proxy.Add(y * 0.5f);

        }

        return proxy;

    }

}

[Serializable]
public class GridData
{

    public int numOfRays;

    public List<float> gridX = new List<float>();
    public List<float> gridY = new List<float>();

}