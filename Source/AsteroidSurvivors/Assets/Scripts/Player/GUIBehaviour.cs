using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBehaviour : MonoBehaviour {

    public GameObject MenuBuildings;
    public GameObject BuildButton;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void HideMenuBuildings()
    {
        BuildButton.SetActive(true);
        MenuBuildings.SetActive(false);
    }
    public void ShowMenuBuildings()
    {
        MenuBuildings.SetActive(true);
        BuildButton.SetActive(false);
    }

    public void ShowAir()
    {
        Debug.Log("AIR");
    }
    public void ShowPower()
    {
        Debug.Log("Power");
    }
    public void ShowWater()
    {
        Debug.Log("Water");
    }

    public void ShowCharacters()
    {

    }
}
