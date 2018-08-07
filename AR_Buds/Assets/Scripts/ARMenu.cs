using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenu : MonoBehaviour
{
    public enum MENU_TYPE { Liiv }
    public enum MENU_STATE { NOT_INITIALIZED, INITIALIZED }

    public GameObject menuLogoLiivPrefab;
    private GameObject m_centralLogoObj;
    private LogoAnimateable m_logoAnimateable;

	void Start ()
    {
        // no current implementation
	}
	
    public void Init(MENU_TYPE menuType)
    {
        Debug.Log("Initializing logo");

        // build the prefabs
        BuildAndSetupMainLogo(menuType);
    }

    private void BuildAndSetupMainLogo(MENU_TYPE menuType)
    {
        if(menuType == MENU_TYPE.Liiv)
            m_centralLogoObj = Instantiate(menuLogoLiivPrefab, transform.position, transform.rotation) as GameObject;
        else // TODO: other menu types will be instantiated here
            m_centralLogoObj = Instantiate(menuLogoLiivPrefab, transform.position, transform.rotation) as GameObject;

        m_centralLogoObj.transform.SetParent(transform, true);

        // get ref to logo's script
        m_logoAnimateable = m_centralLogoObj.transform.GetComponent<LogoAnimateable>();
        Debug.Log("Got logo strip " + (m_logoAnimateable != null ? "true" : "false"));
        // Start the logo animation
        m_logoAnimateable.SetAnimationState(LogoAnimateable.ANIM_STATE.ANIM_IN);
    }
	
	void Update ()
    {
		
	}
}
