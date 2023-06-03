using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image img;
    [SerializeField] AudioClip click;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(click);
    }
}
