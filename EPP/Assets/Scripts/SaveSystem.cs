using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save (StoreMenu store)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:/Games/DeadEndSave";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data();

        formatter.Serialize(stream, data);
        stream.Close();
        
    }

    public static Data Load()
    {
        string path = Application.persistentDataPath + "/save.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;

        } else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
