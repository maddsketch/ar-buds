using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleButton : MonoBehaviour, IPointerDownHandler
{
    public delegate void TapAction(string label);
    public static event TapAction OnTap;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log("Button was clicked!!!");

        if(OnTap != null)
        {
            OnTap(gameObject.name);
        }

        
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
