using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{

    public GameObject PauseGO;
    public AudioMixer mixer;

    public Toggle SoundToggle;
    public Toggle FullscreenToggle;

    void OnEnable()
    {
        if(!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetInt("volume", 1);

        if (!PlayerPrefs.HasKey("fullscreen"))
            PlayerPrefs.SetInt("fullscreen", 1);

        if (PlayerPrefs.GetInt("volume") == 1)
           SoundToggle.isOn = true;
        else
           SoundToggle.isOn = false;

        if (PlayerPrefs.GetInt("fullscreen") == 1)
            FullscreenToggle.isOn = true;
        else
            FullscreenToggle.isOn = false;
    }

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
        if (!state)
        {
            mixer.SetFloat("Volume", -80f);
            PlayerPrefs.SetInt("volume",0);
        }
        else
        {
            mixer.SetFloat("Volume", 0);
            PlayerPrefs.SetInt("volume", 1);
        }

        PlayerPrefs.Save();
    }

    public void ToggleBackground(GameObject bg)
    {
        bg.GetComponent<Image>().enabled = !bg.GetComponent<Image>().enabled;
    }

    public void ToggleFullscreen(bool state)
    {
        Screen.fullScreen = state;

        if (!state)
            PlayerPrefs.SetInt("fullscreen", 0);
        else
            PlayerPrefs.SetInt("fullscreen", 1);

        PlayerPrefs.Save();
    }

}
