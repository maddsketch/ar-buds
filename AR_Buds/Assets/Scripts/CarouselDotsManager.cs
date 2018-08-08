using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselDotsManager : MonoBehaviour
{
    public CarouselDot[] dots;

    private int m_currentIndex = -1;


    public void SetCurrentIndex(int index)
    {
        Debug.Log("Index passed: " + index);

        if ((index >= 0) && (index < (dots.Length)))
        {
            m_currentIndex = index;
            UpdateDots(index);
        }
    }

    private void UpdateDots(int index)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            if (i == index)
                dots[i].SetState(CarouselDot.DOT_STATE.ON);
            else
                dots[i].SetState(CarouselDot.DOT_STATE.OFF);
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
