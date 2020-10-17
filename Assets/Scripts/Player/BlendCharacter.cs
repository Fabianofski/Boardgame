using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCharacter : MonoBehaviour
{

    public Sprite[] BlendStates;
    public float BlendTime;

    private float TimePerState;
    private int Index;
    private SpriteMask spritemask;

    void Awake()
    {
        TimePerState = BlendTime / (BlendStates.Length - 1);
        spritemask = GetComponent<SpriteMask>();
    }

    public void HideCharacter()
    {
        Index = BlendStates.Length - 1;
        StartCoroutine(SetStates(-1));
    }

    public void RevealCharacter()
    {
        Index = 0;
        StartCoroutine(SetStates(1));
    }

    IEnumerator SetStates(int i)
    {
        spritemask.sprite = BlendStates[Index];

        Index += i;
        yield return new WaitForSeconds(TimePerState);

        if (Index < BlendStates.Length && Index >= 0)
            StartCoroutine(SetStates(i));
        else if (Index == -1)
            Invoke("RevealCharacter", 0.25f);
        
        

        
    }

}
