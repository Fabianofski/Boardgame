using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    public enum CardType { TextCard, SpecificCard }

    [Header("Card Content")]
    public Card[] TextCards;
    public AudioSource CardShowingUp;
    public AudioSource CardHiding;
}
