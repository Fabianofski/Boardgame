using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualCameraMovement : MonoBehaviour
{

    float ScrollWheelInput;

    [Header("Scrolling")]
    public float Scrollspeed;
    public Vector2 maxSizes;
    public Slider ZoomSlider;
    private bool ScrollingWithSlider;

    [Header("Camera Dragging")]
    private Vector3 oldMousePos;

    void Update()
    {
        if (!ScrollingWithSlider)
        {

            if(Input.touchCount < 2)
                 HandleDragging();
            HandleScrolling();
        }
    }

    private void HandleDragging()
    {
        if(Input.GetMouseButtonDown(0))
            oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 dir = oldMousePos - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position += dir;
        }

        oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void HandleScrolling()
    {
        PCScrolling();
    }

    private void PCScrolling()
    {
        ScrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if ((Camera.main.orthographicSize > maxSizes.x && ScrollWheelInput > 0) || (Camera.main.orthographicSize < maxSizes.y && ScrollWheelInput < 0))
            Camera.main.orthographicSize -= ScrollWheelInput * Scrollspeed * Time.deltaTime;

        ZoomSlider.value = Camera.main.orthographicSize;
    }


    public void SliderZoomScrolling()
    {
        Camera.main.orthographicSize = ZoomSlider.value;
    }

    public void SliderPressed()
    {
        ScrollingWithSlider = true;
    }

    public void SliderUnPressed()
    {
        ScrollingWithSlider = false;
    }
}
