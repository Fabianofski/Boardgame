using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    public enum CardType { TextCard, SpecificCard }

    [Header("Card Content")]
    public List<Card> TextCards;
    public List<Card> UsedTextCards;
    public AudioSource CardShowingUp;
    public AudioSource CardHiding;

    public void CheckIfAllCardsUsed()
    {
        if(TextCards.Count == 0)
        {
            TextCards = UsedTextCards;
            UsedTextCards = new List<Card>();
        }
    }
}
