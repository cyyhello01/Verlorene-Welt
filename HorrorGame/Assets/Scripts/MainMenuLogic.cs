using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject optionMenu;
    private GameObject loading;

    public AudioSource buttonSound;



    void Start()
    {
        mainMenu = GameObject.Find("MainMenuCanvas");
        optionMenu = GameObject.Find("OptionCanvas");
        loading = GameObject.Find("LoadingCanvas");

        mainMenu.GetComponent<Canvas>().enabled = true;
        optionMenu.GetComponent<Canvas>().enabled = false;
        loading.GetComponent<Canvas>().enabled = false;
    }

    public void StartButton()
    {
        loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("GameplayScene");
    }

    public void OptionButton()
    {
        buttonSound.Play();
        mainMenu.GetComponent<Canvas>().enabled = false;
        optionMenu.GetComponent<Canvas>().enabled = true;
    }

    public void ExitGameButton()
    {
        buttonSound.Play();
        Application.Quit();
        Debug.Log("Game Has Exited");
    }

    public void ReturnToMainMenuButton()
    {
        buttonSound.Play();
        mainMenu.GetComponent<Canvas>().enabled = true;
        optionMenu.GetComponent<Canvas>().enabled = false;
    }


    void Update()
    {

    }
}