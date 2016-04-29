using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBehaviour : MonoBehaviour {

    public GameObject MenuBuildings;
    public GameObject BuildButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CloseMenuBuildings()
    {
        BuildButton.SetActive(true);
        MenuBuildings.SetActive(false);
    }
    public void OpenMenuBuildings()
    {
        MenuBuildings.SetActive(true);
        BuildButton.SetActive(false);
    }
}
