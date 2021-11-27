using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public Transform target;

    public float zoomFactor = 3f;
    public float zoomLerpSpeed = 10f;
    public float zoomLimit = 5f;

    private float defaultTargetzoom;
    private float targetzoom;

    void Awake()
    {
        cam = Camera.main;
        targetzoom = cam.orthographicSize;
        defaultTargetzoom = targetzoom;
    }

    void Start()
    {
        target = PlayerController.instance.playerList[0].gameObject.transform;
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
        targetzoom = Mathf.Clamp(targetzoom, defaultTargetzoom - zoomLimit, defaultTargetzoom + zoomLimit);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetzoom, zoomLerpSpeed * Time.deltaTime);
    }
}