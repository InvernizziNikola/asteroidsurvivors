using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunkerCell : MonoBehaviour {


    private List<BunkerCell> neighbours = new List<BunkerCell>() { null, null, null, null };

    private Bunker bunker;

	// Use this for initialization
	void Start ()
    {
        FindNeighbours();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FindNeighbours()
    {
        BunkerCell bc;
        //get top neighbour if possible
        if (bunker.BunkerCells.TryGetValue(gameObject.transform.position + new Vector3(0, 0, 1), out bc))
        {
            neighbours[(int)Neighbour.Above] = bc;
            bc.neighbours[(int)Neighbour.Below] = this;
        }
        else
            neighbours[(int)Neighbour.Above] = null;

        //get left neighbour if possible
        if (bunker.BunkerCells.TryGetValue(gameObject.transform.position + new Vector3(-1, 0, 0), out bc))
        {
            neighbours[(int)Neighbour.Left] = bc;
            bc.neighbours[(int)Neighbour.Right] = this;
        }
        else
            neighbours[(int)Neighbour.Left] = null;

        //get right neighbour if possible
        if (bunker.BunkerCells.TryGetValue(gameObject.transform.position + new Vector3(1, 0, 0), out bc))
        {
            neighbours[(int)Neighbour.Right] = bc;
            bc.neighbours[(int)Neighbour.Left] = this;
        }
        else
            neighbours[(int)Neighbour.Right] = null;

        //get bottom neighbour if possible
        if (bunker.BunkerCells.TryGetValue(gameObject.transform.position + new Vector3(0, 0, -1), out bc))
        {
            neighbours[(int)Neighbour.Below] = bc;
            bc.neighbours[(int)Neighbour.Above] = this;
        }
        else
            neighbours[(int)Neighbour.Below] = null;

        /*
        int count = 0;
        foreach(BunkerCell n in neighbours)
        {
            if(n != null)
                count++;
        }
        Debug.Log(count);
        Debug.Log(neighbours.Count);
        */
    }
    public void SetBunker(Bunker b)
    {
        bunker = b;
        gameObject.transform.parent = bunker.gameObject.transform;
    }
}
