using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class SaveSystem 
{
    public static readonly string path = Application.persistentDataPath + "/Player.siuuu";

    public static void Saveplayer (Player player )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        DataStore data = new DataStore(player.Stats, player.transform, QuestManager.instance, Inventory.instance);
        data.Coins = CoinsManager.instance.Coins;

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static DataStore LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataStore data = formatter.Deserialize(stream) as DataStore;
            stream.Close();

            Debug.Log(" Load thành công từ file: " + path); // Thêm log kiểm tra

            return data;
        }
        else
        {
            Debug.Log("không thấy file");
            return null;
        }
    }

}