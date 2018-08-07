using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMenu : MonoBehaviour
{
    public enum MENU_TYPE { Liiv }
    public enum MENU_STATE { JUST_LOGO, SHOWING_CONTROLS }

    private MENU_STATE m_currState;

    public GameObject menuLogoLiivPrefab;
    private GameObject m_centralLogoObj;
    private LogoAnimateable m_logoAnimateable;

    public GameObject[] menuControls;
    private ARCategoryControl[] m_menuControls;

    void OnEnable()
    {
        GestureManager.OnARLogoControlClicked += HandleLogoSelected;
        GestureManager.OnARMenuControlClicked += HandleCategorySelected;
    }

    void OnDisable()
    {
        GestureManager.OnARLogoControlClicked -= HandleLogoSelected;
        GestureManager.OnARMenuControlClicked -= HandleCategorySelected;
    }

    void Start ()
    {
        // set refs to control scripts
        int length = menuControls.Length;
        m_menuControls = new ARCategoryControl[length];
        for (int i = 0; i < menuControls.Length; i++)
        {
            m_menuControls[i] = menuControls[i].transform.GetComponent<ARCategoryControl>();
        }

    }
	
    public void Init(MENU_TYPE menuType)
    {
        Debug.Log("Initializing logo");

        // build the prefabs
        BuildAndSetupMainLogo(menuType);

        // set current state to open
        m_currState = MENU_STATE.JUST_LOGO;
    }

    private void BuildAndSetupMainLogo(MENU_TYPE menuType) // TODO: don't need the type here... just prefab whatever's attached to the script
    {
        if(menuType == MENU_TYPE.Liiv)
            m_centralLogoObj = Instantiate(menuLogoLiivPrefab, transform.position, transform.rotation) as GameObject;
        else // TODO: other menu types will be instantiated here
            m_centralLogoObj = Instantiate(menuLogoLiivPrefab, transform.position, transform.rotation) as GameObject;

        m_centralLogoObj.transform.position = new Vector3(m_centralLogoObj.transform.position.x, m_centralLogoObj.transform.position.y, m_centralLogoObj.transform.position.z + 0.035f);
        m_centralLogoObj.transform.SetParent(transform, true);
        

        // get ref to logo's script
        m_logoAnimateable = m_centralLogoObj.transform.GetComponent<LogoAnimateable>();
        Debug.Log("Got logo strip " + (m_logoAnimateable != null ? "true" : "false"));
        // Start the logo animation
        m_logoAnimateable.SetAnimationState(LogoAnimateable.ANIM_STATE.ANIM_IN);
    }
	
    private void HandleLogoSelected(string controlName)
    {
        if(m_currState == MENU_STATE.JUST_LOGO)
        {
            m_currState = MENU_STATE.SHOWING_CONTROLS;
            StartCoroutine(AnimateInARControls());
        }
    }

    private void HandleCategorySelected(string controlName)
    {

    }

    IEnumerator AnimateInARControls()
    {
        if (menuControls.Length == 3) // Liiv menu
        {
            m_menuControls[0].SetAnimationState(ARCategoryControl.CONTROL_STATE.ANIMATING_IN);
            yield return new WaitForSeconds(0.1f);
            m_menuControls[2].SetAnimationState(ARCategoryControl.CONTROL_STATE.ANIMATING_IN);
            yield return new WaitForSeconds(0.1f);
            m_menuControls[1].SetAnimationState(ARCategoryControl.CONTROL_STATE.ANIMATING_IN);
        }

        yield return null;
    }

    void Update ()
    {
		
	}
}
