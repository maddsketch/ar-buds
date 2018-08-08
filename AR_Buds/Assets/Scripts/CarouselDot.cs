using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselDot : MonoBehaviour
{
    public enum DOT_STATE { OFF, ON }
    private DOT_STATE dotState;
        
    public Sprite offSprite;
    public Sprite onSprite;

    private Image dotImage;

    void Start()
    {
        dotImage = transform.GetComponent<Image>();
        dotState = DOT_STATE.OFF;
        SetImagePerState(DOT_STATE.OFF);
    }

    public void SetState(DOT_STATE state)
    {
        Debug.Log("My State: " + state);

        dotState = state;
        SetImagePerState(state);
    }

    private void SetImagePerState(DOT_STATE state)
    {
        if (state == DOT_STATE.OFF)
            dotImage.sprite = offSprite;
        else
            dotImage.sprite = onSprite;
    }
}
