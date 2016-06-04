using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bunker : MonoBehaviour {

    private Dictionary<Vector3, GameObject> bunkerCells = new Dictionary<Vector3, GameObject>();

    public GameObject cellPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Build(Vector3 coordinates)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab) as GameObject;
        cell.transform.position = coordinates;

        bunkerCells.Add(coordinates, cell);
    }

    public GameObject GetCellFromCoordinates(Vector3 c)
    {
        GameObject outGO;

        if (bunkerCells.TryGetValue(c, out outGO))
            return outGO;

        return null;
    }
}
