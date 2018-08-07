using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARContainerManager : MonoBehaviour
{    
    public GameObject ARMenu_Liiv_Prefab;

    private Vector3 m_SpawnLocation;
    private Quaternion m_SpawnRotation;

    private bool m_IsMenuBuilt = false;

    void OnEnable()
    {
        m_SpawnLocation = transform.position;
        m_SpawnRotation = transform.rotation;
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

        ARMenu arMenu = arMenuLiivObj.transform.GetComponent<ARMenu>();
        arMenu.Init(ARMenu.MENU_TYPE.Liiv);
      
    }

    private void ShowAllMenuComponents()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        foreach (var component in rendererComponents)
            component.enabled = true;
    }

	void Update ()
    {
		
	}
}
