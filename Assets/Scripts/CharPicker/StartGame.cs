using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public Animator anim;

    public GameObject pop;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
        MainMenu.SetActive(!MainMenu.activeSelf);
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
            
        StartCoroutine(PickRandomChar());

        if (PlayerPrefs.GetString("language") == "")
        {
            PlayerPrefs.SetString("language", "EN");
            PlayerPrefs.SetInt("languageValue", 0);
        }

        FindObjectOfType<TMP_Dropdown>().value = PlayerPrefs.GetInt("languageValue");
        FindObjectOfType<TMP_Dropdown>().RefreshShownValue();
    }

    IEnumerator PickRandomChar()
    {
        int random = Random.Range(1, 10);
        Debug.Log(random);

        anim.transform.position = new Vector3(anim.transform.position.x - 10, anim.transform.position.y, anim.transform.position.z);

        for(int i = 0; i < random; i++)
        {
            anim.SetTrigger("next");
            yield return new WaitForSeconds(0.001f);
        }

        anim.transform.position = new Vector3(anim.transform.position.x + 10, anim.transform.position.y, anim.transform.position.z);
    }

    public void PopSound()
    {
        Instantiate(pop);
    }

    public void SwitchLanguage(TMP_Dropdown dropdown)
    {
        PlayerPrefs.SetString("language", dropdown.options[dropdown.value].text);
        PlayerPrefs.SetInt("languageValue", dropdown.value);
        Debug.Log(dropdown.options[dropdown.value].text);
        PlayerPrefs.Save();

        UILocalizer[] uiObjects = FindObjectsOfType<UILocalizer>();

        foreach(UILocalizer ui in uiObjects)
        {
            ui.SwitchLanguage();
        }
    }
}
