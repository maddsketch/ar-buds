using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARContainerManager : MonoBehaviour
{
    public delegate void StateChangeAction(AppStateManager.SCREEN_STATE screenState);
    public static event StateChangeAction OnStateChange;

    public GameObject ARMenu_Liiv_Prefab;
    public GameObject[] ARSlideshowContainerPrefabs;

    private ARMenu _activeARMenu = null;
    private ARSlideshowContainer _activeSlideshowContainer = null;

    private AppStateManager appStateManager;

    public enum SLIDESHOW_INDEX : int // NOTE: Indices should match the prefabs in the array: ARSlideshowContainerPrefabs
    {
        LIIV_HYBRID = 0,
        LIIV_INDICA = 1,
        LIIV_SATIVA = 2
    }

    private Vector3 m_SpawnLocation;
    private Quaternion m_SpawnRotation;

    private bool m_IsMenuBuilt = false;

    

    void OnEnable()
    {
        m_SpawnLocation = transform.position;
        m_SpawnRotation = transform.rotation;

        // attach event handlers
        GestureManager.OnARMenuControlClicked += HandleARMenuSelection;
        SimpleButton.OnTap += HandleSimpleButtonClick;
    }

    void OnDisable()
    {
        // remove event handlers
        GestureManager.OnARMenuControlClicked -= HandleARMenuSelection;
        SimpleButton.OnTap -= HandleSimpleButtonClick;
    }

    void Start ()
    {
        appStateManager = AppStateManager.stateManager;
    }

    private void HandleSimpleButtonClick(string buttonName)
    {
        //Debug.Log("Clicked button name: " + buttonName);

        if (buttonName.Equals("Back_Button"))
        {
            //_activeARMenu.gameObject.SetActive(true);
            SpawnMenu();
            Destroy(_activeSlideshowContainer.gameObject);
        }
        else if (buttonName.Equals("Reset_Button"))
        {
            m_IsMenuBuilt = false;

            if (_activeARMenu != null)
                Destroy(_activeARMenu.gameObject);

            if(_activeSlideshowContainer != null)
                Destroy(_activeSlideshowContainer.gameObject);
                        
        }
    }

    public void SetTrackingFound(bool isTrackingFound)
    {
        // do spawn logic here

        Debug.Log("Container manager got tracking found message!!");
        if (!m_IsMenuBuilt)
            SpawnMenu();
        else
            ShowAllMenuComponents();
        
    }

    private void SpawnMenu()
    {
        m_IsMenuBuilt = true;
        // Spawn the AR Menu and intitialize
        GameObject arMenuLiivObj = Instantiate(ARMenu_Liiv_Prefab) as GameObject;

        arMenuLiivObj.transform.SetParent(transform, false);
        arMenuLiivObj.transform.position = transform.position;
        arMenuLiivObj.transform.rotation = transform.rotation;

        _activeARMenu = arMenuLiivObj.transform.GetComponent<ARMenu>();
        _activeARMenu.Init(ARMenu.MENU_TYPE.Liiv);

        appStateManager.CurrentScreenState = AppStateManager.SCREEN_STATE.MENU;
        if (OnStateChange != null)
            OnStateChange(AppStateManager.SCREEN_STATE.MENU);
    }

    private void ShowAllMenuComponents()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }

    private void HandleARMenuSelection(string menuControlName)
    {
        int menuIndex = -1; // NOTE: negative 1 counts as a flag - if menuControlName not matched, do nothing

        if(menuControlName.Equals("Liiv_Category_Hybrid_Obj"))       
            menuIndex = (int)SLIDESHOW_INDEX.LIIV_HYBRID;        
        else if(menuControlName.Equals("Liiv_Category_Indica_Obj")) // CHANGE LATER TO CORRECT INDEX!!!
            menuIndex = (int)SLIDESHOW_INDEX.LIIV_HYBRID;        
        else if (menuControlName.Equals("Liiv_Category_Sativa_Obj")) // CHANGE LATER TO CORRECT INDEX!!!
            menuIndex = (int)SLIDESHOW_INDEX.LIIV_HYBRID;

        if (menuIndex > -1)
        {
            BuildProductSlideshow(menuIndex);
            appStateManager.CurrentScreenState = AppStateManager.SCREEN_STATE.SLIDESHOW;
            if (OnStateChange != null)
                OnStateChange(AppStateManager.SCREEN_STATE.SLIDESHOW);

            StartCoroutine(TransitionToSlideshow());
        }
    }


    private void BuildProductSlideshow(int slideshowIndex)
    {
        GameObject slideshowContainer = Instantiate(ARSlideshowContainerPrefabs[slideshowIndex]) as GameObject;
        slideshowContainer.transform.SetParent(transform, false);
        _activeSlideshowContainer = slideshowContainer.transform.GetComponent<ARSlideshowContainer>();
    }

    private IEnumerator TransitionToSlideshow()
    {
        yield return new WaitForSeconds(0.05f); // give it a brief pause to make sure everyone's ready to go

        // transition the menu
        Destroy(_activeARMenu.gameObject);

        // transition the slideshow container
        _activeSlideshowContainer.ShowFirst();
    }

    void Update ()
    {
		
	}
}
