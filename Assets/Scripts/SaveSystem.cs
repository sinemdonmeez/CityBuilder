using UnityEngine;
using System.IO;

public static class SaveSystem 
{
    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/
    private static string directoryPath;

    private static bool initialized;
    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
    public static void Init() 
    {
        directoryPath = $"{Application.dataPath}/Saves/";

        if (!Directory.Exists(directoryPath)) 
        {
            Directory.CreateDirectory(directoryPath);
        }

        initialized = true;
    }

    public static void Save(SaveableObjectInScene DataToSave, string FileName)
    {
        if (!initialized) 
        {
            Init();
        }

        string JSONSave = JsonUtility.ToJson(DataToSave);

        if (File.Exists($"{directoryPath}{FileName}.json")) 
        {
            File.Delete($"{directoryPath}{FileName}.json");
        }

        StreamWriter SaveFileWriter = new StreamWriter($"{directoryPath}{FileName}.json");
        SaveFileWriter.WriteLine(JSONSave);
        SaveFileWriter.Close();
    }

    public static void Load<SaveableObjectInScene>(out SaveableObjectInScene LoadedData, string FileName)
    {
        if (!initialized)
        {
            Init();
        }

        StreamReader SaveFileReader = new StreamReader($"{directoryPath}{FileName}.json");
        
        string JSONSave = SaveFileReader.ReadLine();
        LoadedData= JsonUtility.FromJson<SaveableObjectInScene>(JSONSave);
        SaveFileReader.Close();
    }
    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
}
