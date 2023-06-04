using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitEvent : MonoBehaviour
{
    public void QuitGame()
    {
        EditorApplication.isPlaying= false;
    }
}
