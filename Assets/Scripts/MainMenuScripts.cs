using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScripts : MonoBehaviour
{
    public GameObject blackPanel;
    public Button playButton;
    public GameObject menuItems;
    public GameObject optionsPanel;
    public GameObject levelPanel;

    public AudioMixer audioMixer;
    public AudioClip audioClip;
    AudioSource audioSrc;

        
    private void Start()
    {
        blackPanel.GetComponent<CanvasRenderer>().SetAlpha(0);
        blackPanel.GetComponent<CanvasRenderer>().SetAlpha(1);
        Fade(0);
        optionsPanel.SetActive(false);
        levelPanel.SetActive(false);
        audioSrc = GetComponent<AudioSource>();
        blackPanel.SetActive(false);
        audioSrc.clip = audioClip;
    }

    private void Update()
    {
    }

    public void changeMaster(float value)
    {
        audioMixer.SetFloat("Master", value);
    }

    public void changeMusic(float value)
    {
        audioMixer.SetFloat("Music", value);
    }

    public void changeFX(float value)
    {
        audioMixer.SetFloat("FX", value);
    }

    public void toggleMaster(bool value)
    {
        audioMixer.SetFloat("Master", value ? 0 : 1);
    }

    public void toggleMusic(bool value)
    {
        audioMixer.SetFloat("Music", value ? 0 : 1);
    }

    public void toggleFX(bool value)
    {
        audioMixer.SetFloat("FX", value ? 0 : 1);
    }

    public void toggleOptions(bool value)
    {
        if (value == true)
        {
            menuItems.SetActive(false);
            optionsPanel.SetActive(true);
        }
        else
        {
            menuItems.SetActive(true);
            optionsPanel.SetActive(false);
        }

    }

    public void toggleLevelSelect(bool value)
    {
        if (value == true)
        {
            menuItems.SetActive(false);
            levelPanel.SetActive(true);
        }
        else
        {
            menuItems.SetActive(true);
            levelPanel.SetActive(false);
        }

    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneCoroutine(sceneIndex));
    }

    public IEnumerator GoToSceneCoroutine(int sceneIndex)
    {
        audioSrc.Play();
        Fade(1f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void Fade(float toAlpha)
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().CrossFadeAlpha(toAlpha, 2.0f, false);
    }

    public void ExitGame()
    {
        audioSrc.Play();
        Application.Quit();
    }
}
