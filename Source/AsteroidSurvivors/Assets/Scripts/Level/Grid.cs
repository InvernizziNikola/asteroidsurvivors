using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Grid : MonoBehaviour {
 
    public GameObject BunkerPrefab;
    public GameObject PreviewPrefab;

    private Plane plane = new Plane(Vector3.up, 0);


    private static Grid gridInstance;
    public static Grid GetInstance
    {
        get
        {
            if (gridInstance == null)
                gridInstance = (new GameObject("Grid")).AddComponent<Grid>();

            return gridInstance;
        }
    }

    void Awake()
    {
        gridInstance = this;
    }


    private Bunker selectedBunker;

    public Bunker SelectedBunker
    {
        get { return selectedBunker; }
        set { selectedBunker = value; }
    }
    
    private List<Bunker> BunkerList = new List<Bunker>();




    public Bunker CreateFirstBunker()
    {
        GameObject bunker = GameObject.Instantiate(BunkerPrefab) as GameObject;

        Bunker b = bunker.GetComponent<Bunker>();
        SelectedBunker = b;

        b.CreateCell(new Vector3(0, 0, 0));

        return b;
    }

    public void AddBunker(Bunker newBunker)
    {
        BunkerList.Add(newBunker);
    }




















    public void CreatePreview()
    {
        GameObject preview = GameObject.Instantiate(PreviewPrefab) as GameObject;
    }

    public GameObject CreateBuilding()
    {
        return null;
    }











    public BunkerCell GetCellOnMousePosition()
    {
        if (selectedBunker == null)
            return null;

        Bunker bunker = selectedBunker.GetComponent<Bunker>();
        if (bunker == null)
            return null;

        return bunker.GetCellFromCoordinates(GetMousePos());
    }

    public Vector3 GetMousePos()
    {
        Vector3 mousePos = new Vector3();

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out dist))
        {
            mousePos = ray.GetPoint(dist);
        }
        if (mousePos.y != 0)
            Debug.Log("Something fishy in mousepos!" + mousePos);

        mousePos = new Vector3(Mathf.Round(mousePos.x), 0, Mathf.Round(mousePos.z));

        return mousePos;
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

