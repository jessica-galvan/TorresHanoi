using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseOption;

    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button quit;


    private bool winMenuActive;
    private bool pauseMenuActive;

    void Start()
    {
        playAgain.onClick.AddListener(OnClickReloadGameHandler);
        restart.onClick.AddListener(OnClickReloadGameHandler);
        resume.onClick.AddListener(OnClickResumeHandler);
        quit.onClick.AddListener(OnClickQuitHandler);
        pauseButton.onClick.AddListener(OnClickMenuHandler);

        winScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        pauseOption.SetActive(true);
        winMenuActive = false;
        pauseMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !winMenuActive)
        {
            if (!pauseMenuActive)
            {
                Pause();
            }
            else
            {
                ExitMenu();
            }
        }
    }

    private void OnClickMenuHandler()
    {
        Pause();
    }

    private void OnClickReloadGameHandler()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnClickResumeHandler()
    {
        ExitMenu();
    }
    private void OnClickQuitHandler()
    {
        Application.Quit();
        print("Cerramos el juego");
    }

    private void ExitMenu()
    {
        pauseMenuActive = false;
        winScreen.SetActive(false);
        menuScreen.SetActive(false);
        pauseOption.SetActive(true);
        gameScreen.SetActive(true);
    }

    private void Pause()
    {
        pauseMenuActive = true;
        pauseOption.SetActive(false);
        winScreen.SetActive(false);
        menuScreen.SetActive(true);
        gameScreen.SetActive(true);
    }

    public void Winner()
    {
        winMenuActive = true;
        winScreen.SetActive(true);
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
    }
}
