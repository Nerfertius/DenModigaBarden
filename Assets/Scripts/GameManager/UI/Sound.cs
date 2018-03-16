using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, ISelectHandler, ISubmitHandler {

    public AudioClip click, select;
    public bool ignoreFirstSelect;

    public void OnPointerDown(PointerEventData eventData)
    {
		AudioManager.PlayOneShot(click);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.PlayOneShot(select);
    }
	
	public void OnSubmit(BaseEventData eventData){
		AudioManager.PlayOneShot(click);
	}
	
	public void OnSelect(BaseEventData eventData){
        if (ignoreFirstSelect)
        {
            ignoreFirstSelect = false;
            return;
        }
		AudioManager.PlayOneShot(select);
	}
}
