using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class PlayerInputs : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {

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

    public void ShowBuildingList()
    {

    }
    public void ShowMiners()
    {

    }
}
