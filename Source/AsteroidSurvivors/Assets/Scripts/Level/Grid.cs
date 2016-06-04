using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Grid {

    private static Grid gridInstant;
    public static Grid GetInstant
    {
        get
        {
            if (gridInstant == null)
                gridInstant = new Grid();
            return gridInstant;
        }
    }


    private GameObject selectedBunker;

    public GameObject SelectedBunker
    {
        get { return selectedBunker; }
        set { selectedBunker = value; }
    }


    private Grid()
    {
        // private constructor so nobody can create a new grid
    }
    
    private List<GameObject> BunkerList = new List<GameObject>();

    public GameObject GetCellFromCoordinates(Vector3 coordinates)
    {
        if (selectedBunker == null)
            return null;

        Bunker bunker = selectedBunker.GetComponent<Bunker>();

        return bunker.GetCellFromCoordinates(coordinates);
    }

    public GameObject CreateBunker(GameObject prefab)
    {
        GameObject bunker = GameObject.Instantiate(prefab) as GameObject;

        return AddBunker(bunker); 
    }

    public GameObject AddBunker(GameObject newBunker)
    {
        BunkerList.Add(newBunker);

        return newBunker;
    }


    public void Load(GridData gridData)
    {
        
    }


    /*
    public GridData Save()
    {
        GridData gridData = new GridData();

        // save asteroid
       foreach(GameObject astGO in AsteroidList)
        {
            //gridData.Asteroids.Add(astGO.GetComponent<Asteroid>().Save());
        }

        // add more stuff that is child of the grid and needs to be saved

        return gridData;
    }
    */
}

