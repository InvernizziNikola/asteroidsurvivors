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
        if (selectedAsteroid == null)
            return null;

        Asteroid astScript = selectedAsteroid.GetComponent<Asteroid>();
        if (astScript == null)
            return null;

        return astScript.GetCellFromCoordinates(coordinates);
    }

    public GameObject CreateAsteroid(GameObject asteroidPrefab, AstSize size)
    {
        GameObject tempAsteroid = MonoBehaviour.Instantiate(asteroidPrefab) as GameObject;
        tempAsteroid.GetComponent<Asteroid>().AsteroidSize = size;
        tempAsteroid.transform.position = new Vector3(-5, 5);

        AddAsteroid(tempAsteroid);

        return tempAsteroid;
    }

    public void AddAsteroid(GameObject newAsteroid)
    {
        AsteroidList.Add(newAsteroid);
    }


    public void Load(GridData gridData)
    {
        foreach(AsteroidData astData in gridData.Asteroids)
        {

        }
    }


    public GridData Save()
    {
        GridData gridData = new GridData();

        // save asteroid
        foreach(GameObject astGO in AsteroidList)
        {
            gridData.Asteroids.Add(astGO.GetComponent<Asteroid>().Save());
        }

        // add more stuff that is child of the grid and needs to be saved

        return gridData;
    }
}

