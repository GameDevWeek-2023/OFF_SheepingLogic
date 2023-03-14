using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScripts : MonoBehaviour
{
    public GameObject blackPanel;
    private bool fadeScreen = false;

    private void Update()
    {
        if (fadeScreen)
        {
            blackPanel.GetComponent<Image>().CrossFadeAlpha(1, 2.0f, false);
        }

    }

    public void GoToScene(string sceneName)
    {
        fadeScreen= true;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
