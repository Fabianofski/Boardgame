using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public string tagname;
    public bool CheckForOther;
    public bool DestroyObject;
    public float time;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
        if (DestroyObject)
            Invoke("Destroy", time);

        CheckForOtherObjects();
    }

    private void CheckForOtherObjects()
    {
        if (!CheckForOther)
            return;

        GameObject[] go = GameObject.FindGameObjectsWithTag(tagname);

        if (go.Length > 1)
            Destroy(gameObject);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
