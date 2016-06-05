using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunkerCell : MonoBehaviour {


    private List<GameObject> neighbours = new List<GameObject>();
    private Bunker bunker;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FindNeighbours()
    {
    
    }
    public void SetBunker(Bunker b)
    {
        bunker = b;
        gameObject.transform.parent = bunker.gameObject.transform;
    }
}
