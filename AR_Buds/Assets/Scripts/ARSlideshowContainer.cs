using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSlideshowContainer : MonoBehaviour
{
    public enum CONTAINER_STATE { IDLE, ANIMATING_LEFT, ANIMATING_RIGHT }
    private CONTAINER_STATE m_currContainerState = CONTAINER_STATE.IDLE;

    public ARProductContainer[] productContainers;
    private Vector3 m_carouselLeftPos;
    private Vector3 m_carouselViewPos;
    private Vector3 m_carouselRigthPos;
    private float k_carouselPositionSpacerSize = 20.0f;
    private float k_animSpeed = 100.0f;

    // for animations
    private GameObject m_leftAnimatingProduct = null;
    private GameObject m_rightAnimatingProduct = null;

    private List<GameObject> m_leftCarouselObjects = new List<GameObject>();
    private GameObject m_inViewCarouselObject;
    private List<GameObject> m_rightCarouselObjects = new List<GameObject>();

    void OnEnable()
    {
        // Set Positions for placing product containers within the slideshow
        m_carouselViewPos = transform.position;
        m_carouselLeftPos = new Vector3(m_carouselViewPos.x - k_carouselPositionSpacerSize, m_carouselViewPos.y, m_carouselViewPos.z);
        m_carouselRigthPos = new Vector3(m_carouselViewPos.x + k_carouselPositionSpacerSize, m_carouselViewPos.y, m_carouselViewPos.z);
    }

    void Start ()
    {       
        // push all containers to the right
        SetProductContainersOpenState();
    }
	
    private void SetProductContainersOpenState()
    {
        // move all containers to the right pos
        for(int i = 0; i < productContainers.Length; i++)
        {
            GameObject container = productContainers[i].gameObject;
            container.transform.position = m_carouselRigthPos;
            // add them all to the right carousel object list
            m_rightCarouselObjects.Add(container);
        }
    }
	
    // animates the first object into postion
    public void ShowFirst()
    {
        m_currContainerState = CONTAINER_STATE.ANIMATING_LEFT;
        //** set pointers
        // note: left point is null
        m_rightAnimatingProduct = productContainers[0].gameObject;
    }

	void Update ()
    {
		if((m_currContainerState == CONTAINER_STATE.ANIMATING_LEFT) || (m_currContainerState == CONTAINER_STATE.ANIMATING_RIGHT))
        {
            float direction = (m_currContainerState == CONTAINER_STATE.ANIMATING_LEFT ? -1.0f : 1.0f);
            float nextMove = direction * Time.deltaTime * k_animSpeed;

            if(m_leftAnimatingProduct != null)
            {
                float nextXPos = m_leftAnimatingProduct.transform.position.x + nextMove;
                if (m_currContainerState == CONTAINER_STATE.ANIMATING_LEFT)
                {
                    if (nextXPos <= m_carouselLeftPos.x)
                        nextXPos = m_carouselLeftPos.x;
                }
                else // animating right
                {
                    if (nextXPos >= m_carouselLeftPos.x)
                        nextXPos = m_carouselLeftPos.x;
                }

                m_leftAnimatingProduct.transform.position = new Vector3(nextXPos, m_leftAnimatingProduct.transform.position.y, m_leftAnimatingProduct.transform.position.z);
            }

            if(m_rightAnimatingProduct != null)
            {
                float nextXPos = m_rightAnimatingProduct.transform.position.x + nextMove;
                if (m_currContainerState == CONTAINER_STATE.ANIMATING_LEFT)
                {
                    if (nextXPos <= m_carouselViewPos.x)
                    {
                        nextXPos = m_carouselViewPos.x;                        
                        m_currContainerState = CONTAINER_STATE.IDLE; // stop animation
                        // swap pointer refs and clean up list if necessary
                        m_inViewCarouselObject = m_rightAnimatingProduct;
                        if (m_rightCarouselObjects.Count > 0)
                            m_rightCarouselObjects.RemoveAt(m_rightCarouselObjects.Count - 1); // pop last off list
                        m_leftCarouselObjects.Add(m_leftAnimatingProduct);

                    }
                }
                else // Animating right
                {
                    if (nextXPos >= m_carouselViewPos.x)
                    {
                        nextXPos = m_carouselViewPos.x;
                        m_currContainerState = CONTAINER_STATE.IDLE; // stop animation
                        // swap pointer refs and clean up list if necessary
                        m_inViewCarouselObject = m_leftAnimatingProduct;
                        if(m_leftCarouselObjects.Count > 0)
                            m_leftCarouselObjects.RemoveAt(m_leftCarouselObjects.Count - 1); // pop last off list
                        m_rightCarouselObjects.Add(m_rightAnimatingProduct);

                    }
                }
                m_rightAnimatingProduct.transform.position = new Vector3(nextXPos, m_rightAnimatingProduct.transform.position.y, m_rightAnimatingProduct.transform.position.z);
            }
            else
            {
                // stop animation -- DEFAULT in case something broke and couldn't find references
                m_currContainerState = CONTAINER_STATE.IDLE;
            }
        }
	}
}
