using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCategoryControl : MonoBehaviour
{
    public enum CONTROL_STATE { OPEN_HIDDEN, ANIMATING_IN, FULLY_VISIBLE, ANIMATING_OUT_SELECTED, ANIMATING_OUT_NOT_SELECTED }
    private CONTROL_STATE m_currState;
    public CONTROL_STATE ControlState
    {
        get { return m_currState; }
    }

    private float m_targetYPos;
    private const float k_startYPosOffset = 10.0f;
    private const float k_movementSpeed = 50.0f;
    private float m_targetScale; // gets applied to all axese

	void Start ()
    {
        m_targetYPos = transform.position.y;
        m_targetScale = transform.localScale.x;

        SetDefaultState();
	}
	
    // move down, set scale control size way down, turn off renderers
    private void SetDefaultState()
    {
        m_currState = CONTROL_STATE.OPEN_HIDDEN;

        //move position
        transform.position = new Vector3(transform.position.x, transform.position.y - k_startYPosOffset, transform.position.z);
        // adjust scale
        transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        // turn off renderer
        MeshRenderer controlRenderer = transform.GetComponentInChildren<MeshRenderer>();
        controlRenderer.enabled = false;
    }

    public void SetAnimationState(CONTROL_STATE controlState)
    {
        m_currState = controlState;
        if(m_currState == CONTROL_STATE.ANIMATING_IN)
        {
            MeshRenderer controlRenderer = transform.GetComponentInChildren<MeshRenderer>();
            controlRenderer.enabled = true;
        }
    }
	
	void Update ()
    {
	    if(m_currState == CONTROL_STATE.ANIMATING_IN)
        {
            UpdateAnimateIn(Time.deltaTime);
        }
	}

    // animates the open animation
    private void UpdateAnimateIn(float deltaTime)
    {
        if (transform.position.y < m_targetYPos)
        {
            float nextY = transform.position.y + (deltaTime * k_movementSpeed);
            if (nextY >= m_targetYPos)
                nextY = m_targetYPos;

            transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
        }

        if(transform.localScale.x < m_targetScale)
        {
            float nextScale = transform.localScale.x + (deltaTime * 0.025f);
            if (nextScale >= m_targetScale)
                nextScale = m_targetScale;

            transform.localScale = new Vector3(nextScale, nextScale, nextScale);
        }

        if((transform.position.y == m_targetYPos) && (transform.localScale.x == m_targetScale))
        {
            m_currState = CONTROL_STATE.FULLY_VISIBLE;
        }
    }
}
