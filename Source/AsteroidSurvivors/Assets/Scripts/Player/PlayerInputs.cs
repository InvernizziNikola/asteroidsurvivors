using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{
    private float targetZoom = 15;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        bool overUI = EventSystem.current.IsPointerOverGameObject();

        if (!overUI)
        {

            /*if (Input.GetMouseButtonDown(0))
            {

                GameObject mouseOverCell = Grid.GetInstance.GetCellOnMousePosition();

                if (mouseOverCell == null)
                {
                    if(Grid.GetInstance.SelectedBunker != null)
                        Grid.GetInstance.SelectedBunker.GetComponent<Bunker>().CreateCell(Grid.GetInstance.GetMousePos());
                }

            }*/

            CameraMovement();
            CameraZoomInOut();
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            Grid.GetInstance.CreatePreview();
            Player.GetInstant.PlayerState = PlayerState.Building;
        }
    }

    void CameraMovement()
    {
        Camera.main.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 10.0f;
    }
    void CameraZoomInOut()
    {
        float newfov = Input.GetAxis("Mouse ScrollWheel") * -5;
        targetZoom += newfov;
        targetZoom = Mathf.Clamp(targetZoom, 1, 20);
        Camera.main.orthographicSize = Mathf.Lerp(targetZoom, Camera.main.orthographicSize, 0.97f);
    }
}
