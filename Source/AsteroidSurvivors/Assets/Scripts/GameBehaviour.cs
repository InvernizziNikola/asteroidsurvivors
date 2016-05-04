using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {

    public GameObject prefabAsteroid;

    // Use this for initialization
    void Start ()
    {
        if (prefabAsteroid != null)
        {
            GameObject tempAsteroid = MonoBehaviour.Instantiate(prefabAsteroid) as GameObject;
            tempAsteroid.transform.position = new Vector3(10, 0);

            // just make the first asteroid the selectedasteroid!
            Grid.GetInstant.SelectedAsteroid = tempAsteroid;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
