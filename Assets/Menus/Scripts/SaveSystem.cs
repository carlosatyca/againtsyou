using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static string buttonPressed = "";
    public static void SavePlayer (PlayerHealth player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = System.IO.Path.Combine(Application.persistentDataPath, "saveData.cartom");

        PlayerData data = new PlayerData(player);

        PlayerHealth.startTime = Time.time;
        PlayerHealth.playedTime = data.playedTime;

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static PlayerData LoadPlayer ()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "saveData.cartom");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    PlayerData data = formatter.Deserialize(stream) as PlayerData;
                    return data;
                } catch (Exception e)
                {
                    Debug.LogError("Error deserializando el archivo de guardado");
                    Debug.LogError(e);
                    return null;
                }

            }
        }
        else
        {
            Debug.LogError("Archivo de guardado no encontrado en " + path);
            return null;
        }
    }
}
