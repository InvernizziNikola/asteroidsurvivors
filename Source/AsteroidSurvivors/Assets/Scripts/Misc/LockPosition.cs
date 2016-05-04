using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class LockPosition : MonoBehaviour
{

    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("CheckPosition", 2, 2);
    }

    // Update is called once per frame

    void CheckPosition()
    {
        if (startPos.Equals(transform.position))
            return;

        transform.position = startPos;
        Debug.Log("Position from " + name + " has been changed, while it shouldn't!");
    }
}
