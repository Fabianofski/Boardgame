using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    Material _material;
    float Hcurrentscroll;
    float Vcurrentscroll;
    public float Hspeed;
    public float Vspeed;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Hcurrentscroll += Hspeed * Time.deltaTime;
        Vcurrentscroll += Vspeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(Hcurrentscroll, Vcurrentscroll);
    }
}
