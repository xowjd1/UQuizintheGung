using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotationSpeed = 200f;

    private float mx = 0f;
    private float my = 0f;
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        // Debug.Log($"x:{mouseX}, y:{mouseY}");

        mx += mouseX * rotationSpeed * Time.deltaTime;
        my += mouseY * rotationSpeed * Time.deltaTime;
        
        my = Mathf.Clamp(my, -90f, 90f);

        transform.eulerAngles = new Vector3(-my, mx, 0f);
    }
}
