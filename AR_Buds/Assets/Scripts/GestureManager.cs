﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public Camera ARCamera;

    public delegate void ARControlClick(GameObject tappedControl);
    public static event ARControlClick OnARControlClicked;

    public delegate void ARLogoControlClick(string controlName);
    public static event ARLogoControlClick OnARLogoControlClicked;

    public delegate void ARMenuControlClick(string controlName);
    public static event ARMenuControlClick OnARMenuControlClicked;

    public delegate void ARLearnMoreControlClick(string controlName);
    public static event ARLearnMoreControlClick OnARLearnMoreControlClicked;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                string tagName = hit.collider.tag;
                if (tagName == "ARControl")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Control");

                    if (OnARControlClicked != null)
                    {
                        OnARControlClicked(tappedControl);
                    }
                }
                else if (tagName == "ARControl_Logo")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Logo Control: " + tappedControl.name);

                    if (OnARLogoControlClicked != null)
                    {
                        OnARLogoControlClicked(tappedControl.name);
                    }
                }
                else if(tagName == "ARControl_Menu_Category")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    ARCategoryControl control = tappedControl.transform.GetComponent<ARCategoryControl>();
                    Debug.Log("Tapped an AR Menu Control: " + tappedControl.name);
                    if (control.CanTap)
                    {
                        control.CanTap = false;

                        if (OnARMenuControlClicked != null)
                        {
                            OnARMenuControlClicked(tappedControl.name);
                        }
                    }
                }
                else if (tagName == "ARControl_LearnMore")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Learn More Control: " + tappedControl.name);

                    if (OnARLearnMoreControlClicked != null)
                    {
                        OnARLearnMoreControlClicked(tappedControl.name);
                    }
                }

                if (tagName == "Menu_Row")
                {
                    Debug.Log("Tapped a menu row...");
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            //Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
            Ray ray = ARCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string tagName = hit.collider.tag;
                if (tagName == "ARControl")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Control");

                    if (OnARControlClicked != null)
                    {
                        OnARControlClicked(tappedControl);
                    }
                }
                else if (tagName == "ARControl_Logo")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Logo Control: " + tappedControl.name);

                    if (OnARLogoControlClicked != null)
                    {
                        OnARLogoControlClicked(tappedControl.name);
                    }
                }
                else if (tagName == "ARControl_Menu_Category")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    ARCategoryControl control = tappedControl.transform.GetComponent<ARCategoryControl>();
                    Debug.Log("Tapped an AR Menu Control: " + tappedControl.name);
                    if (control.CanTap)
                    {
                        control.CanTap = false;

                        if (OnARMenuControlClicked != null)
                        {
                            OnARMenuControlClicked(tappedControl.name);
                        }
                    }
                }
                else if (tagName == "ARControl_LearnMore")
                {
                    GameObject tappedControl = hit.collider.gameObject;
                    Debug.Log("Tapped an AR Learn More Control: " + tappedControl.name);

                    if (OnARLearnMoreControlClicked != null)
                    {
                        OnARLearnMoreControlClicked(tappedControl.name);
                    }
                }
            }
        }

    } // end update
} // end class
