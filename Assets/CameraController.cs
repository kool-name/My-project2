using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MoveSpeed;
    public float zoomSpeed;
    public float RotationSpeed;
    public Vector3 CameraOriginPos;
    public Vector3 CameraOriginRot;
    private Vector3 rotation;
    private Vector3 motion;
    private Vector3 zoom;
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform.Find("MainCamera");
        cam.transform.localPosition = CameraOriginPos;

    }

    // Update is called once per frame
    void Update()
    {

        motion = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetButton("Fire3"))
        {

            cam.transform.RotateAround(transform.localPosition,transform.right, Input.GetAxis("Mouse Y") *RotationSpeed* Time.deltaTime);
            rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            transform.Rotate(-rotation * RotationSpeed * Time.deltaTime);


        }                                                                   //Camera rotation

        zoom = new Vector3(0,0,Input.GetAxisRaw("Mouse ScrollWheel"));      //Camera Zoom
        cam.transform.Translate(zoom * zoomSpeed * Time.deltaTime);
        transform.Translate(motion * MoveSpeed * Time.deltaTime);           //Camera translation
        //cam.transform.LookAt(transform.localPosition);
    }
 
}
