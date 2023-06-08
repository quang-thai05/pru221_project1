using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hint;
    // Start is called before the first frame update
    void Start()
    {
        hint.enabled = false;
    }

    public void OnMouseEnter()
    {
        hint.enabled = true;
    }

    public void OnMouseExit()
    {
        hint.enabled = false;
    }
    public void OnMouseDown() 
    {
        hint.enabled = false;
    }
}
