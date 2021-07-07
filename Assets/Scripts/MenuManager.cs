using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI hiscoreNamesText;
    public TextMeshProUGUI hiscoresText;
    public Text enterNameText;

    public void Start()
    {
        UpdateScores();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnCurrentNameChanged(string newName)
    {
        HiscoreManager.Instance.currentScoreName = enterNameText.text;
    }


    public void UpdateScores()
    {
        string hiscoreNames = "";
        string hiscoreScores = "";

        foreach(HiscoreManager.Hiscore hiscore in HiscoreManager.Instance.hiscores)
        {
            hiscoreNames += hiscore.name + "\n";
            hiscoreScores += hiscore.score + "\n";
        }

        hiscoreNamesText.text = hiscoreNames;
        hiscoresText.text = hiscoreScores;
    }
}
