using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScripts : MonoBehaviour
{
    public GameObject blackPanel;
    public Button playButton;
    public AudioClip audioClip;
    AudioSource audioSrc;

        
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        blackPanel.SetActive(false);
        audioSrc.clip = audioClip;
        blackPanel.GetComponent<CanvasRenderer>().SetAlpha(0);
        playButton.onClick.AddListener(() => StartCoroutine("GoToScene"));
    }

    private void Update()
    {
    }

    public IEnumerator GoToScene()
    {
        audioSrc.Play();
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().CrossFadeAlpha(1, 1.0f, false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ExitGame()
    {
        audioSrc.Play();
        Application.Quit();
    }
}
