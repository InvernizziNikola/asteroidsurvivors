﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingPreview : MonoBehaviour {


    public List<GameObject> cells = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Grid.GetInstance.GetMousePos();
	}
}
