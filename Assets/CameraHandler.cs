using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {
 

    public float mouseLookSpeedX = 3;
    public float mouseLookSpeedY= 0.05f;

    public bool zoomToggle = false;

    Camera cam;

    float xRotation = 0;
    float camYDis= 0;


    Vector3 offset;
    Quaternion rotOffset;

    bool zoom = false;

    //Function to be used to limit the angle you can look
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < min)
            angle = min;
        if (angle > max)
            angle = max;
        return Mathf.Clamp(angle, min, max);
    }


	
    void Awake()
    {
        cam = Camera.main;
        offset = cam.transform.localPosition;
        rotOffset = cam.transform.localRotation;
    }
	
	void LateUpdate () {
       



        //Looks up and down and turns left and right
       
            camYDis = Input.GetAxis("Mouse Y") * mouseLookSpeedY;
            xRotation = Input.GetAxis("Mouse X") * mouseLookSpeedX;
            
            if(!zoom)
            {
                cam.transform.Translate(0, camYDis, 0);
            }
           
            transform.Rotate(0, xRotation, 0);


            if (cam.transform.localPosition.y < -0.5)
            {
                cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, -0.5f, cam.transform.localPosition.z);
            }

            if (cam.transform.localPosition.y > 2.5)
            {
                cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, 2.5f, cam.transform.localPosition.z);
            }

          
            if(cam.transform.localPosition.z != -2)
            {
                cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -2);
            }



            //Zoom control
            if (zoomToggle)
            {
                if (Input.GetButtonDown("ToggleZoom"))
                {
                    
                    if (zoom)
                    {
                        zoom = false;
                        cam.transform.localPosition = offset;
                        
                     }
                    else
                    {
                        offset = cam.transform.localPosition;
                        zoom = true;
                        cam.transform.localRotation = rotOffset;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if(!zoom)
                    {
                        offset = cam.transform.localPosition;
                        zoom = true;
                        cam.transform.localRotation = rotOffset;
                    }
                }
                if(Input.GetMouseButtonUp(1))
                {
                    if (zoom)
                    {
                        zoom = false;
                        cam.transform.localPosition = offset;
                     cam.transform.localRotation = rotOffset;
                }
            }
            }

            if(zoom)
            {
                cam.transform.localPosition = Vector3.Slerp(cam.transform.localPosition, new Vector3(0.7f, 0.9f, -1.17f), 0.1f);
                cam.transform.localRotation.eulerAngles.Set(26.8f, 0, 0);
            }
            else
            {
                cam.transform.LookAt(transform);
            }

        
        

    }
}
