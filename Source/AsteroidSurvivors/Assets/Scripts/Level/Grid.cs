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
    
    private GameObject selectedAsteroid;
    public GameObject SelectedAsteroid
    {
        get
        {
            return selectedAsteroid;
        }
        set
        {
            selectedAsteroid = value;
        }
    }

    private Grid()
    {
        // private constructor so nobody can create a new grid
    }

    private List<GameObject> AsteroidList = new List<GameObject>();

    public GameObject GetCellFromCoordinates(Position coordinates)
    {
        Asteroid astScript = selectedAsteroid.GetComponent<Asteroid>();
        if (astScript == null)
            return null;

        return astScript.GetCellFromCoordinates(coordinates);
    }
    public void AddAsteroid(GameObject newAsteroid)
    {
        AsteroidList.Add(newAsteroid);
    }

    public void Load()
    {

    }
    public void Save()
    {
       
    }
}

