using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mycamera;
    public float zoomSpeed = 0.1f;
    public float minZoom = -20f;
    public float maxZoom = 20f;

    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0)
        {
            mycamera.orthographicSize -= zoomSpeed;
        } else if(scroll < 0)
        {
            mycamera.orthographicSize += zoomSpeed;
        }
        mycamera.orthographicSize = Mathf.Clamp(mycamera.orthographicSize, minZoom, maxZoom);
    }
}
