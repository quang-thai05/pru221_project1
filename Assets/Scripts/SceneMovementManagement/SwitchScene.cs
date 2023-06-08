using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    private string maingame = "GameScene";
    private string mainmenu = "MainMenu";
    private string description = "AboutUsScene";
    [Header("Panels")]
    [SerializeField] private GameObject pausepanel;
   

    public void Start()
    {
        pausepanel.SetActive(false);
    }

    public void SwitchtoMainGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(maingame);
    }
    public void SwitchtoPause()
    {
        Time.timeScale = 0f;
        pausepanel.SetActive(true);
    }
    public void SwitchtoMainMenu()
    {
        SceneManager.LoadScene(mainmenu);
    }
    public void BackToGame()
    {
        Time.timeScale = 1f;
        pausepanel.SetActive(false);
    }
    public void SwitchtoDescription()
    {
        SceneManager.LoadScene(description);
    }
    public void CloseDescription()
    {
        SceneManager.LoadScene(mainmenu);
    }
    public void SaveGames()
    {
        SaveLoad.SaveGame();
    }
    public void LoadGames()
    {
        Time.timeScale = 1f;
        SaveLoad.LoadGame();
        SceneManager.LoadScene(maingame);
    }
    
}
