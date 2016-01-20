using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {
    public enum RotationAxes {MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float mouseLookSpeed = 5;

    public float zoomAmount = 50;

    Camera cam;

    float xRotation = 0;
    float yRotation = 0;

    public int horizLookDegrees = 90;
    public int vertUpDegrees = 80;
    public int vertDownDegrees = 45;

    public bool toggleZoom = true;

    Quaternion originalRotation;
    Quaternion originalCharRotation;


    bool lookBack = false;
    bool zoomedIn = false;

    //Function to be used to limit the angle you can look
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < min)
            angle = min;
        if (angle > max)
            angle = max;
        return Mathf.Clamp(angle, min, max);
    }

    // Use this for initialization
    void Start () {
	
	}
	
    void Awake()
    {
        cam = Camera.main;
        originalRotation = cam.transform.localRotation;
        originalCharRotation = transform.localRotation;
    }
	// Update is called once per frame
	void Update () {

        if (!lookBack)
        {

            //Looks up and down and turns left and right
            if (axes == RotationAxes.MouseXAndY)
            {
                yRotation += Input.GetAxis("Mouse Y") * mouseLookSpeed;
                yRotation = ClampAngle(yRotation, -vertDownDegrees, vertUpDegrees);

                xRotation += Input.GetAxis("Mouse X") * mouseLookSpeed;
                xRotation = ClampAngle(xRotation, -360, 360);

                Quaternion yQuaternion = Quaternion.AngleAxis(-yRotation, Vector3.right);
                Quaternion xQuaternion = Quaternion.AngleAxis(xRotation, Vector3.up);
                cam.transform.localRotation = originalRotation * yQuaternion;
                transform.localRotation = originalCharRotation * xQuaternion;

            }


            //Turns left and right
            else if (axes == RotationAxes.MouseX)
            {

                xRotation += Input.GetAxis("Mouse X") * mouseLookSpeed;
                xRotation = ClampAngle(xRotation, -360, 360);
                Quaternion xQuaternion = Quaternion.AngleAxis(xRotation, Vector3.up);
                transform.localRotation = originalCharRotation * xQuaternion;
            }
        }
      
        

        //Look Back Control
        if (Input.GetButtonDown("LookBack"))
        {
            if(lookBack == false)
            {
                
                lookBack = true;
            }
        }
        if(Input.GetButtonUp("LookBack"))
        {
            if(lookBack == true)
            {
                lookBack = false;
            }   
        }

        if(lookBack == true)
        {
            cam.transform.localRotation = new Quaternion(0, 180 , 0, cam.transform.localRotation.w);  
        }

        //Zoom Control

        //Check if zoom is toggle or not
        if(toggleZoom)
        {
            if (Input.GetButtonDown("ToggleZoom"))
            {
                zoomedIn = !zoomedIn;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomedIn = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                zoomedIn = false;
            }
        }
       



        if (zoomedIn)
        {
            cam.fieldOfView = zoomAmount;
        }
        if(!zoomedIn)
        {
            cam.fieldOfView = 60;
        }
        
    }
}
