using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string firstLevel;
    public GameObject ControllerScene;
    public GameObject MainMenuScene;
    public GameObject HowToPlayScene;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene(firstLevel);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
    public void Controller()
    {
        MainMenuScene.SetActive(false);
        ControllerScene.SetActive(true);
        
    }
    public void BackToMainMenu()
    {
        ControllerScene.SetActive(false);
        HowToPlayScene.SetActive(false);
        MainMenuScene.SetActive(true);
    }
    public void HowToPlay()
    {
        HowToPlayScene.SetActive(true);
        MainMenuScene.SetActive(false);
    }
}