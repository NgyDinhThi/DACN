using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class SaveSystem 
{
    public static void Saveplayer (Player player )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "Player.siuuu";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataStore data = new DataStore(player.Stats, player.transform);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static DataStore LoadPlayer()
    {
        string path = Application.persistentDataPath + "Player.siuuu";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataStore data = formatter.Deserialize(stream) as DataStore;
            stream.Close();
            
            return data;

        }

        else
        {
            Debug.Log("không thấy file");
            return null;
        }
    }

}