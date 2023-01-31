using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScr : MonoBehaviour
{
    public float moveSpeed;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;
    public float rotateSpeed;
    private float curZoom;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
        curXRot = -50;
    }

    // Update is called once per frame
    void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
        cam.transform.localPosition = Vector3.up * curZoom;
    }
}
