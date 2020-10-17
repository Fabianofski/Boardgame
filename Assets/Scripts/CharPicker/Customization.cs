using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customization : MonoBehaviour
{

    public Animator animator;
    public int CharIndex;
    public int TotalCharCount;

    public void NextChar()
    {
        animator.SetTrigger("next");

        if (CharIndex < TotalCharCount - 1)
            CharIndex++;
        else
            CharIndex = 0;
    }

    public void PreviousChar()
    {
        animator.SetTrigger("previous");

        if (CharIndex > 0)
            CharIndex--;
        else
            CharIndex = TotalCharCount - 1;
    }

}
