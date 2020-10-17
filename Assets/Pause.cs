using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{

    public GameObject PauseGO;
    public AudioMixer mixer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        PauseGO.SetActive(!PauseGO.activeSelf);
    }

    public void ToggleSound(bool state)
    {
        if(!state)
            mixer.SetFloat("Volume", -80f);
        else
            mixer.SetFloat("Volume", 0);
    }

    public void ToggleFullscreen(bool state)
    {

        Screen.fullScreen = state;

    }

}
