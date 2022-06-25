using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Execute the script later than all the default scripts
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    // Update method deleted - we don't need it right now

    public ColorPicker ColorPicker;

    public void NewNameSelected(string name)
    {
        // new name typed
        MainManager.Instance.PlayerName = name;
    }

    public void NewColorSelected(Color color)
    {
        // select a new color
        MainManager.Instance.PlayerColor = color;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ColorPicker.Init(); // ABSTRACTION 
        // this will call the NewColorSelected function when the color picker have a color button clicked
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.Instance.PlayerColor); // added to load the saved color
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        MainManager.Instance.SaveName();
        MainManager.Instance.SaveColor();

        // Conditional programming, depending on if it's on the editor or on a standalone build performs different methods to quit the game / application
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.PlayerColor);
    }

    public void SaveNameClicked()
    {
        MainManager.Instance.SaveName();
    }

    public void LoadNameClicked()
    {
        MainManager.Instance.LoadName();
    }
}
