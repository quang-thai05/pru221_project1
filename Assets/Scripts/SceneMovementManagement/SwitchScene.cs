using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private string maingame = "GameScene";
    private string pausescene = "PauseScene";
    private string mainmenu = "MainMenu";

    public void SwitchtoMainGame()
    {
      SceneManager.LoadScene(maingame);
    }
    public void SwitchtoPause()
    {
        SceneManager.LoadScene(pausescene);
    }
    public void SwitchtoMainMenu()
    {
        SceneManager.LoadScene(mainmenu);
    }
}
