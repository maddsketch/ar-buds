using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject swipeCanvas;
    public ProductCalloutCanvasManager calloutCanvasManager;
    public GameObject uiBackButton;
    public GameObject uiResetButton;

    void OnEnable()
    {
        GestureManager.OnARLearnMoreControlClicked += HandleLearnMoreClick;
        SimpleButton.OnTap += HandleSimpleButtonClick;
        ARContainerManager.OnStateChange += HandleAppStateChange;
    }

    void OnDisable()
    {
        GestureManager.OnARLearnMoreControlClicked -= HandleLearnMoreClick;
        SimpleButton.OnTap -= HandleSimpleButtonClick;
        ARContainerManager.OnStateChange -= HandleAppStateChange;
    }

    // Use this for initialization
    void Start ()
    {
        // hide the product callout by default
        calloutCanvasManager.Show(false);
        //hide the swipe canvas by default
        swipeCanvas.SetActive(false);

        // hide the buttons by default
        uiBackButton.SetActive(false);
        uiResetButton.SetActive(false);

    }
	
    private void HandleLearnMoreClick(string productName)
    {
        Debug.Log("Trapped Learn more in UI manager");

        string header = "";
        string subheader = "";
        string content = "";

        Debug.Log("Handled product name: " + productName);

        if(productName.Equals("Learn_More_Button(Bali_Kush)"))
        {
            header = "Bali Kush";
            subheader = "Hybrid";
            content = "Indica-dominant hybrid that houses a dense, olive coloured bud with dark orange pistils. Gives off an earthy, citrus aroma with hints of hops and clove.";
            calloutCanvasManager.Show(true);
            calloutCanvasManager.SetContent(header, subheader, content);
        }
        else if (productName.Equals("Learn_More_Button(Budda_Haze)"))
        {
            header = "Budda Haze";
            subheader = "Hybrid";
            content = "This sativa-dominant hybrid is a true green bud with a THC coating.Small orange hairs enhance the floral, honey-like lemony aroma of the flower.";
            calloutCanvasManager.Show(true);
            calloutCanvasManager.SetContent(header, subheader, content);
        }
        else if (productName.Equals("Learn_More_Button(Clarity_Coast)"))
        {
            Debug.Log("Setting Clarity Coast!!!");
            header = "Clarity Coast";
            subheader = "Hybrid";
            content = "Hybrid strain with airy, deep green buds and cider - coloured pistils that create an olive tone. Floral and citrus aromas complement the refreshing scents of pine and hops.";
            calloutCanvasManager.Show(true);
            calloutCanvasManager.SetContent(header, subheader, content);
        }
    }

    private void HandleSimpleButtonClick(string buttonName)
    {
        Debug.Log("Clicked button name: " + buttonName);

        if(buttonName.Equals("Close_Product_Info_Button"))
        {
            calloutCanvasManager.Show(false);
        }
    }

    private void HandleAppStateChange(AppStateManager.SCREEN_STATE screenState)
    {
        if(screenState == AppStateManager.SCREEN_STATE.MENU)
        {
            swipeCanvas.SetActive(false);
            uiResetButton.SetActive(true);
            uiBackButton.SetActive(false);
        }
        else if (screenState == AppStateManager.SCREEN_STATE.SLIDESHOW)
        {
            swipeCanvas.SetActive(true);
            uiResetButton.SetActive(true);
            uiBackButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
