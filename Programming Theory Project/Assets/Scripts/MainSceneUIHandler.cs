using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainSceneUIHandler : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainSceneUIHandler Instance { get; private set; } // ENCAPSULATION (4 Pillars of OOP)

    public TextMeshProUGUI CurrentPlayerName;

    private string PlayerName;

    private void Awake()
    {
        Instance = this;

        MainManager.Instance.LoadName(); // ABSTRACTION
    }

    void Start()
    {
        CurrentPlayerName.text = MainManager.Instance.PlayerName;
        //PlayerName = MainManager.Instance.PlayerName;
        //CurrentPlayerName.text = $" {PlayerName} ";
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        // Conditional programming, depending on if it's on the editor or on a standalone build performs different methods to quit the game / application
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
