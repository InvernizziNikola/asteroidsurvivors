using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bunker : MonoBehaviour {


    private Dictionary<Vector3, BunkerCell> bunkerCells = new Dictionary<Vector3, BunkerCell>();

    public Dictionary<Vector3, BunkerCell> BunkerCells
    {
        get { return bunkerCells; }
    }

    public List<GameObject> Buildings = new List<GameObject>();


    public GameObject cellPrefab;


    void Awake()
    {
        Grid.GetInstance.AddBunker(this);

    }
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	


	}


    public BunkerCell CreateCell(Vector3 coordinates)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab, coordinates, Quaternion.Euler(90,0,0)) as GameObject;

        BunkerCell c = cell.GetComponent<BunkerCell>();
        
        bunkerCells.Add(coordinates, c);
        c.SetBunker(this);
        
        return c;
    }

    public BunkerCell GetCellFromCoordinates(Vector3 c)
    {
        BunkerCell bunkerCell;

        if (bunkerCells.TryGetValue(c, out bunkerCell))
            return bunkerCell;

        return null;
    }
}
