using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraToggler : MonoBehaviour
{

    private CameraMovement AutoCamMovement;
    private ManualCameraMovement ManCamMovement;

    public Slider ZoomSlider;

    void Awake()
    {
        AutoCamMovement = GetComponent<CameraMovement>();
        ManCamMovement = GetComponent<ManualCameraMovement>();

        EnableAutomaticCameraMovement();
    }

    public void ToggleCamera()
    {
        AutoCamMovement.enabled = !AutoCamMovement.enabled;
        ManCamMovement.enabled = !ManCamMovement.enabled;

        ZoomSlider.gameObject.SetActive(!ZoomSlider.gameObject.activeSelf);
        ZoomSlider.value = Camera.main.orthographicSize;
    }

    public void EnableManualCameraMovement()
    {
        AutoCamMovement.enabled = false;
        ManCamMovement.enabled = true;

        ZoomSlider.gameObject.SetActive(true);
        ZoomSlider.value = Camera.main.orthographicSize;
    }

    public void EnableAutomaticCameraMovement()
    {
        AutoCamMovement.enabled = true;
        ManCamMovement.enabled = false;
        ZoomSlider.gameObject.SetActive(false);
    }

}
