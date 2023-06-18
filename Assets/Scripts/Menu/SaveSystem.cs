using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using File = System.IO.File;

public class SaveSystem : MonoBehaviour
{
    private const string FileType = ".txt";
    private static string SavePath => Application.persistentDataPath + "/Saves/";
    private static string BackUpSavePath => Application.persistentDataPath + "/BackUps/";

    private static int SaveCount = 0;
    public static void SaveData<T>(T data, string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        if (SaveCount % 5 == 0) Save(BackUpSavePath);
        Save(SavePath);
        Debug.Log("Local kayıt yapıldı.");
        SaveCount++;

        void Save(string path)
        {
            using StreamWriter writer = new(path + fileName + FileType);
            BinaryFormatter formatter = new();
            MemoryStream memoryStream = new();
            formatter.Serialize(memoryStream, data);
            string dataToSave = Convert.ToBase64String(memoryStream.ToArray());
            writer.WriteLine(dataToSave);
        }
    }

    public static T LoadData<T>(string fileName)
    {
        Directory.CreateDirectory(SavePath);
        Directory.CreateDirectory(BackUpSavePath);

        bool backUpNeeded = false;
        T dataToReturn;

        Load(SavePath);
        Debug.Log("Localden veri geldi.");
        if (backUpNeeded) Load(BackUpSavePath);

        return dataToReturn;

        void Load(string path)
        {
            using StreamReader reader = new(path + fileName + FileType);
            BinaryFormatter formatter = new();
            string dataToLoad = reader.ReadToEnd();
            MemoryStream memoryStream = new(Convert.FromBase64String(dataToLoad));

            try
            {
                dataToReturn = (T)formatter.Deserialize(memoryStream);
            }
            catch
            {
                backUpNeeded = true;
                dataToReturn = default;
            }
        }
    }

    public static bool SaveExists(string fileName) =>
        File.Exists(SavePath + fileName + FileType)
        || File.Exists(BackUpSavePath + fileName + FileType);
}
