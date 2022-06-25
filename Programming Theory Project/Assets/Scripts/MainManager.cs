using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainManager Instance { get; private set; } // ENCAPSULATION (4 Pillars of OOP)

    //public Text PlayerName; // need to investigate how to call the type of text, since it's TMPro and not regular text

    public string PlayerName;
    public Color PlayerColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadName(); // ABSTRACTION
        LoadColor(); // ABSTRACTION
    }

    [System.Serializable]
    class SaveData
    {
        //public Text PlayerName; // need to investigate how to call the type of text, since it's TMPro and not regular text
        public string PlayerName;
        public Color PlayerColor;
    }

    // Player Name
    public void SaveName()
    {
        SaveData data = new SaveData();

        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
        }
    }

    // Player Color
    public void SaveColor()
    {
        SaveData data = new SaveData();

        data.PlayerColor = PlayerColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerColor = data.PlayerColor;
        }
    }
}
