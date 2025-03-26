using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
using Newtonsoft.Json;

public class JsonDataService : IDataService
{

    private const string KEY = "XR730KAZKN94gPryb6aqhAmHDVeUXqCaKqamc5FWmYZ=";
    private const string IV = "TgYkpQeXNAG2HQhWJTZ8yd==";

    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {

        string path = Application.persistentDataPath + "/" + RelativePath + ".json";

        try
        {
            
            if (File.Exists(path))
            {

                File.Delete(path);

            }

            using FileStream fileStream = File.Create(path);

            if (Encrypted == true)
            {

                WriteEncryptedData(Data, fileStream);

            }
            else
            {

                fileStream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(Data));

            }

            return true;

        }
        catch (Exception e)
        {

            Debug.LogError($"Unable to save data due to: {e.Message} { e.StackTrace}; script JsonDataService");
            return false;

        }

    }

    private void WriteEncryptedData<T>(T Data, FileStream fileStream)
    {

        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            fileStream,
            cryptoTransform,
            CryptoStreamMode.Write
            );

        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Data)));

    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {

        string path = Application.persistentDataPath + "/" + RelativePath + ".json";

        if (!File.Exists(path))
        {

            Debug.LogError($"Cannot load file at {path}; script JsonDataService. File does not exist!");
            throw new FileNotFoundException($"!ERROR! {path} does not exist; script JsonDataService !ERROR!");

        }

        try
        {

            T data;

            if (Encrypted == true)
            {

                data = ReadEncryptedData<T>(path);

            }
            else
            {

                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

            }

            return data;

        }
        catch (Exception e)
        {

            Debug.LogError($"!ERROR! Failed to load due to: {e.Message} { e.StackTrace}; script JsonDataService ! ERROR!");
            throw e;

        }

    }

    private T ReadEncryptedData<T>(string path)
    {

        byte[] fileBytes = File.ReadAllBytes(path);
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
            aesProvider.Key,
            aesProvider.IV
        );

        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream,
            cryptoTransform,
            CryptoStreamMode.Read
        );

        using StreamReader reader = new StreamReader(cryptoStream);

        string result = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<T>(result);

    }

    public bool DoesFileExist(string RelativePath)
    {

        string path = Application.persistentDataPath + "/" + RelativePath + ".json";

        if (File.Exists(path))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

}
