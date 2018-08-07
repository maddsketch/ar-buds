using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARContainerManager : MonoBehaviour
{    
    public GameObject ARMenu_Liiv_Prefab;
    public GameObject[] ARSlideshowContainerPrefabs;

    private ARMenu _activeARMenu = null;
    private ARSlideshowContainer _activeSlideshowContainer = null;

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
    }

    void OnDisable()
    {
        // remove event handlers
        GestureManager.OnARMenuControlClicked -= HandleARMenuSelection;
    }

    void Start ()
    {
        // no current implementation
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
      
    }

    private void ShowAllMenuComponents()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
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
