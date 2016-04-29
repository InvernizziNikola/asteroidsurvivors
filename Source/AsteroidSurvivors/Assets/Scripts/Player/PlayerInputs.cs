using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{
    public float targetZoom = 15;
    private Plane plane = new Plane(Vector3.forward, 0);

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2();

            float dist;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out dist))
            {
                mousePos = ray.GetPoint(dist);
            }

            mousePos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            
            Debug.Log(mousePos);
            // TODO
            //GameObject mouseOverCell;
            //if (CellList.TryGetValue(new Position((int)mousePos.x, (int)mousePos.y), out mouseOverCell))
            //{
            //    Debug.Log("Selected: " + mouseOverCell.name + " with neighbour cells: " + mouseOverCell.GetComponent<AsteroidCell>().cellN);
            //}
        }
        


        CameraMovement();
        CameraZoomInOut();
    }
    void CameraMovement()
    {
        Camera.main.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * 10.0f;
    }
    void CameraZoomInOut()
    {
        float newfov = Input.GetAxis("Mouse ScrollWheel") * -5;
        targetZoom += newfov;
        targetZoom = Mathf.Clamp(targetZoom, 1, 20);
        Camera.main.orthographicSize = Mathf.Lerp(targetZoom, Camera.main.orthographicSize, 0.95f);
    }
}
