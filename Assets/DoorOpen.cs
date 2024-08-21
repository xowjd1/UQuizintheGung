using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Vector3 openXPosition;
    public Vector3 closedXPosition;

    private float speed = 2.0f;

    void FixedUpdate()
    {
        if (GameManager.Instance.open)
        {
            Vector3 targetPosition = openXPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        } 
        /*
        else
        {
            Vector3 targetPosition = closedXPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
        */
    }
}
