using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ErrorService : IErrorService
{

    const string fileName = "ErrorLog";
    ISystemTime TimeService = new SystemTime();

#nullable enable
    public void LogError(Exception? e)
    {

        string path = Application.persistentDataPath + "/" + fileName + ".json";
        ErrorLog logClass = new ErrorLog();
        logClass.ErrorMessages.Clear();

        if (File.Exists(path))
        {

            ErrorLog data = LoadLog<ErrorLog>();

            for (int i = 0; i < data.ErrorMessages.Count; i++)
            {

                logClass.ErrorMessages.Add(new string(data.ErrorMessages[i]));

            }

            File.Delete(path);
            using FileStream fileStream = File.Create(path);
            fileStream.Close();

        }
        else
        {

            using FileStream fileStream = File.Create(path);
            fileStream.Close();
            logClass.ErrorMessages.Add(new string("This is an error log file, all errors experienced during runtime will be recorded here. If you experience a crash, a glitch, a bug or if there are any entries here besides this, please send a copy of this file to the developer. Their Discord and Email address can be found on their itch io account page. Thank you for your co-operation!"));
            logClass.ErrorMessages.Add(new string("But incase you can't find them here is my Email address: sakkasus@gmail.com"));

        }

        if (e != null)
        {

            string errorMessage = "!ERROR!\n " + e.Message + " \n " + e.StackTrace + " \n " + e.Source + " \n " + TimeService.FullCalendarDateTime() + " | " + TimeService.FullDigitalClockTime();
            logClass.ErrorMessages.Add(errorMessage);

        }
        
        SaveLogFile(logClass);

    }
#nullable disable

    private T LoadLog<T>()
    {

        string path = Application.persistentDataPath + "/" + fileName + ".json";
        T data;
        data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        return data;

    }

    private void SaveLogFile<T>(T Data)
    {

        string path = Application.persistentDataPath + "/" + fileName + ".json";
        File.WriteAllText(path, JsonConvert.SerializeObject(Data));

    }

}
