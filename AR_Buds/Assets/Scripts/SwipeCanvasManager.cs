using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCanvasManager : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public delegate void SwipeAction(SCROLL_DIRECTION scrollDirection);
    public static event SwipeAction OnSwipe;

    public enum SCROLL_DIRECTION { LEFT, RIGHT }
    private SCROLL_DIRECTION currScrollDirection;

    private Vector2 lastPointerPos;

    private bool _canSwipe = false;

    public void OnEndDrag(PointerEventData data)
    {
       
    }

    public void OnDrag(PointerEventData data)
    {
        if (_canSwipe)
        {
            float xDelta = lastPointerPos.x - data.position.x;
            //Debug.Log("Distance traveled: " + xDelta);
            if (Mathf.Abs(xDelta) > 10.0f)
            {
                if (data.position.x < lastPointerPos.x)
                {
                    Debug.Log("Told to scroll left...");
                    if (OnSwipe != null)
                        OnSwipe(SCROLL_DIRECTION.LEFT);
                }
                else
                {
                    Debug.Log("Told to scroll right....");
                    if (OnSwipe != null)
                        OnSwipe(SCROLL_DIRECTION.RIGHT);
                }
                _canSwipe = false; // NOTE: this check is in to prevent re-firing of the event
            }            
        }
    }
    
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Started drag!!!");
        _canSwipe = true;
        lastPointerPos = data.position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
