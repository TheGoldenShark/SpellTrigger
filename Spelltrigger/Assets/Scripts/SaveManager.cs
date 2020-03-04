using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveSaveData(SaveData saveData)
    {
        // This function saves the data to a file, by converting the serialisable savedata class
        // to a binary stream and saving that as a file
        BinaryFormatter formatter = new BinaryFormatter();
        // Generates the file path, by finding a basic path to save data relevant to the OS
        // (on windows this will be \AppData\LocalLow\DefaultCompany\Spelltrigger)
        string path = Application.persistentDataPath + "/savedata.data";

        // Opens the file and saves the serialised class, then closes the file
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
            stream.Close();
        }
    }

    public static SaveData LoadSaveData()
    {
        // This function loads the data from a file, by converting the file back into
        // the serializable savedata class and returning it
        BinaryFormatter formatter = new BinaryFormatter();
        // Generates the file path, by finding a basic path to save data relevant to the OS
        // (on windows this will be \AppData\LocalLow\DefaultCompany\Spelltrigger)
        string path = Application.persistentDataPath + "/savedata.data";
        if (File.Exists(path))
        {
            // Opens the file and deserialises it to get the class, then returns that class
            // Only runs if the file exists
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();
                return data;
            }
        }
        // If a file is not found, it will create a new savedata class
        else
        {
            return new SaveData();
        }
    }
}
