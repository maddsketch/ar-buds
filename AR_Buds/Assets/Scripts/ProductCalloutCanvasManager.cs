using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductCalloutCanvasManager : MonoBehaviour
{
    public GameObject background;

    public Text header;
    public Text subheader;
    public Text content;

	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void SetContent(string header, string subheader, string content)
    {
        this.header.text = header;
        this.subheader.text = subheader;
        this.content.text = content;
    }

    public void Show(bool shouldShow)
    {
        if (shouldShow)
            background.SetActive(true);
        else
            background.SetActive(false);
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
