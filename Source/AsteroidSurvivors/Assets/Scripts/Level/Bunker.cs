using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bunker : MonoBehaviour {

    [SerializeField]
    public Dictionary<Vector3, BunkerCell> bunkerCells = new Dictionary<Vector3, BunkerCell>();

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


    public GameObject CreateCell(Vector3 coordinates)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab, coordinates, Quaternion.identity) as GameObject;

        BunkerCell c = cell.GetComponent<BunkerCell>();
        
        bunkerCells.Add(coordinates, c);
        c.SetBunker(this);

        return cell;
    }

    public BunkerCell GetCellFromCoordinates(Vector3 c)
    {
        BunkerCell bunkerCell;

        if (bunkerCells.TryGetValue(c, out bunkerCell))
            return bunkerCell;

        return null;
    }
}
