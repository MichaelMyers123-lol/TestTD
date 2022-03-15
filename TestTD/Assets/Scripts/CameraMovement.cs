using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThicc= 10f;
    public float ScrollSpeed = 5f;
    public float MinY = 15f;
    public float MaxY = 80f;
    public float MinX = -10f;
    public float MaxX = 80f;
    public float MinZ = -100f;
    public float MaxZ = 10f;
    
    void Awake()
    {
        
}
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.Escape))
        doMovement = !doMovement;
        if (doMovement == false) return;



        if( Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThicc)
        {
            pos.z +=  panSpeed * Time.deltaTime;
            pos.z = Mathf.Clamp(pos.z, MinZ, MaxZ);
            transform.position = pos;
            
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThicc)
        {
            pos.z -= panSpeed * Time.deltaTime;
            pos.z = Mathf.Clamp(pos.z, MinZ, MaxZ);
            transform.position = pos;

            //transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThicc)
        {
            pos.x -= panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, MinX, MaxX);
            transform.position = pos;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThicc)
        {
            pos.x += panSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, MinX, MaxX);
            transform.position = pos;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.y -= scroll * 1000 * ScrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        transform.position = pos;

    }
}
