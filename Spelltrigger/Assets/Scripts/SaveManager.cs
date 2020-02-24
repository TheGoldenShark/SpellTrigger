using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveSaveData(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.data";

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
            stream.Close();
        }
    }

    public static SaveData LoadSaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.data";
        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();
                return data;
            }
        }
        else
        {
            return new SaveData();
        }
    }
}
