using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimateable : MonoBehaviour
{
    public enum ANIM_STATE { HIDDEN, ANIM_IN, SOLID_VISIBLE }
    private ANIM_STATE m_CurrAnimState;
	
    void OnEnable()
    {
        SetDefaultState();
    }

	void Start ()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // By default, the object becomes hidden
    private void SetDefaultState()
    {
        m_CurrAnimState = ANIM_STATE.HIDDEN;
        // hide children
        //ShowHideChildren(false);
    }

    // Shows and hides all child objects. 
    // True to show, false to hide
    private void ShowHideChildren(bool isHidden)
    {
        int numChildren = transform.childCount;
        for(int i = 0; i < numChildren; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isHidden);
        }
    }
    
    public void SetAnimationState(ANIM_STATE animState)
    {
        m_CurrAnimState = animState;
        Debug.Log("Animation state set on logo: " + animState);
    }
	
	void Update ()
    {
		if(m_CurrAnimState == ANIM_STATE.ANIM_IN)
        {
            Debug.Log("Told to animate in the Logo");

            // show all the children
            ShowHideChildren(true);

            // start the animation
            Animator animator = transform.GetComponent<Animator>();
            animator.CrossFade("Collapse_To_Logo", 0.0f);
            // immediately flag as not animating - the anim is pre-built, is only triggered once, does not need to be updated **** CHANGE!!
            m_CurrAnimState = ANIM_STATE.SOLID_VISIBLE;
            // change state
            // make interactible
            // enable the box collider
        }
	}
}
