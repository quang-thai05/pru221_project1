using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private string sceneName = "SampleScene";
    public void Switch()
    {
      SceneManager.LoadScene(sceneName);
    }
}
