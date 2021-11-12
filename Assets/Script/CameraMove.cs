using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera cam;
    public Transform target;

    private float targetzoom;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;

    void Awake()
    {
        cam = Camera.main;
        targetzoom = cam.orthographicSize;
    }

    void Update()
    {
        Tracking();
        Zoom();
    }

    void Tracking()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }

    void Zoom()
    {
        float scrollDate = Input.GetAxis("Mouse ScrollWheel");

        targetzoom -= scrollDate * zoomFactor;
        targetzoom = Mathf.Clamp(targetzoom, 4.5f, 8f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetzoom, zoomLerpSpeed * Time.deltaTime);
    }
}
