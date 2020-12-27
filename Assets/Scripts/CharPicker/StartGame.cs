using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(PickRandomChar());
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
}
