using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public string namePlayer;
    public string name;
    public int maxScore;
    //public 
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMaxScore();
    }
    [System.Serializable]
    class SaveData
    {
        public string namePlayer;
        public int maxScore;
    }
    public void SaveMaxScore()
    {
        SaveData data = new SaveData();

        data.maxScore = maxScore;
        data.namePlayer = namePlayer;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //File.WriteAllText(Application.dataPath + "/savefile.json", json);
        Debug.Log("Saved");
    }
    public void LoadMaxScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            maxScore = data.maxScore;
            namePlayer = data.namePlayer;
        }
    }
}
