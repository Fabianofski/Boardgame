using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterContainer : MonoBehaviour
{

    public Customization[] Chars;
    public GameObject[] CharPrefabs;
    public Button AddButton;

    public GameObject[] CameraScroll;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene") return;

        Chars = FindObjectsOfType<Customization>();

        CameraScroll = new GameObject[Chars.Length + 1];

        for(int i = 0; i < CameraScroll.Length - 1; i++)
        {
            CameraScroll[i] = Chars[i].gameObject;
        }
        CameraScroll[CameraScroll.Length - 1] = AddButton.gameObject;
    }

    public void SpawnPlayers(Vector3 spawnPos)
    {
        foreach(Customization c in Chars)
        {
            Instantiate(CharPrefabs[c.CharIndex], spawnPos, Quaternion.identity);
        }
    }
}
