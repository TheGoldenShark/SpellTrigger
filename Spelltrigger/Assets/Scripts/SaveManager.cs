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
            SaveData data = new SaveData(saveData);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/savedata.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
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
