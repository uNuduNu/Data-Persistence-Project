using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class HiscoreManager : MonoBehaviour
{
    public static HiscoreManager Instance;

    public string currentScoreName;

    public List<Hiscore> hiscores; // score, name

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        hiscores = new List<Hiscore>();

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHiscores();
    }

    private void OnApplicationQuit()
    {
        SaveHiscores();
    }

    [System.Serializable]
    public class Hiscore
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class HiscoresSaveData
    {
        public List<Hiscore> hiscores; // score, name
    }

    public void SaveHiscores()
    {
        HiscoresSaveData data = new HiscoresSaveData();
        data.hiscores = hiscores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Hiscores saved");
        foreach (Hiscore hiscore in hiscores)
        {
            Debug.Log(hiscore.name + " : " + hiscore.score);
        }

        Debug.Log("json : " + json);

    }

    public void LoadHiscores()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HiscoresSaveData data = JsonUtility.FromJson<HiscoresSaveData>(json);

            if (data.hiscores != null)
            {
                hiscores = data.hiscores;
            }
            else 
            {
                hiscores = new List<Hiscore>();
            }

            Debug.Log("Hiscores loaded");
            foreach (Hiscore hiscore in hiscores)
            {
                Debug.Log(hiscore.name + " : " + hiscore.score);
            }
            Debug.Log("json : " + json);
        }
    }

    public void AddScore(int score)
    {
        // keep 5 best scores, arranged by score, best score first
        for (int i = 0; i < hiscores.Count; i++)
        {
            if (score > hiscores[i].score)
            {
                Hiscore newHiscore = new Hiscore();
                newHiscore.name = currentScoreName;
                newHiscore.score = score;

                hiscores.Insert(i, newHiscore);

                if (hiscores.Count > 5)
                {
                    hiscores.RemoveAt(hiscores.Count - 1);
                }

                return;
            }
        }

        if (hiscores.Count < 5)
        {
            Hiscore newHiscore = new Hiscore();
            newHiscore.name = currentScoreName;
            newHiscore.score = score;

            hiscores.Add(newHiscore);
        }
    }

    public string GetBestScore()
    {
        if (hiscores.Count > 0)
        {
            return hiscores[0].name + " : " + hiscores[0].score;
        }

        return " : 0";
    }
}
