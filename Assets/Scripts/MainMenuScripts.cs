using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScripts : MonoBehaviour
{
    public GameObject blackPanel;
    public Button playButton;
        
    private void Start()
    {
        blackPanel.SetActive(false);
        blackPanel.GetComponent<CanvasRenderer>().SetAlpha(0);
        playButton.onClick.AddListener(() => StartCoroutine("GoToScene"));
    }

    private void Update()
    {
    }

    public IEnumerator GoToScene()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().CrossFadeAlpha(1, 1.0f, false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
