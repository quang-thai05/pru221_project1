using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundSound : MonoBehaviour
{
    [SerializeField] AudioClip bgsound;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(bgsound);
    }

   
}
