using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class ActionCard : MonoBehaviour
{
 
    [Header("General Card Setup")]
    public CardContainer.CardType cardtype;
    public GameObject Panel;
    public GameObject CardUI;
    public Image image;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;

    public Button button;
    public Card specificCard;
    public UnityEvent CardFunction;


    private CardActions cardActions;
    private CardContainer cardContainer;

    List<CardLocalization> NormalCards;
    List<CardLocalization> SpecificCards;

    void Start()
    {
        cardContainer = GetComponentInParent<CardContainer>();
        NormalCards = LoadInTSVtoCards("NormalCards");
        SpecificCards = LoadInTSVtoCards("SpecificCards");
    }

    public void ReceiveActionCall()
    {
        Invoke("ShowCard", .5f);
        Invoke("InitializeCard", .5f);
    }


    void ShowCard()
    {
        cardContainer.CardShowingUp.Play();

        CardUI.SetActive(true);
        Panel.SetActive(true);
        CardUI.transform.localScale = Vector3.zero;
        LeanTween.scale(CardUI, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
    }

    void InitializeCard()
    {
        int index;

        List<CardLocalization> CardList = new List<CardLocalization>();
        Card card = ScriptableObject.CreateInstance("Card") as Card;

        switch (cardtype)
        {
            case CardContainer.CardType.TextCard:
                index = Random.Range(0, cardContainer.TextCards.Count);
                card = cardContainer.TextCards[index];
                cardContainer.UsedTextCards.Add(cardContainer.TextCards[index]);
                cardContainer.TextCards.RemoveAt(index);
                cardContainer.CheckIfAllCardsUsed();
                CardList = NormalCards;
                break;

            case CardContainer.CardType.SpecificCard:
                card = specificCard;
                CardList = SpecificCards;
                break;
        }

        string language = PlayerPrefs.GetString("language");

        TitleText.text = (string)CardList[card.ID - 1].GetType().GetField("Title_" + language).GetValue(CardList[card.ID - 1]);
        DescriptionText.text = (string)CardList[card.ID - 1].GetType().GetField("Description_" + language).GetValue(CardList[card.ID - 1]);

        image.sprite = card.CardSprite;
        Panel.GetComponent<Image>().color = card.Color;

        button.onClick.AddListener(CardFunction.Invoke);
    }

    
    List<CardLocalization> LoadInTSVtoCards(string filename)
    {
        List<CardLocalization> CardList = new List<CardLocalization>();
        TextAsset LocalizationText = Resources.Load<TextAsset>(filename);

        string[] data = LocalizationText.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { '\t' });

            CardLocalization c = new CardLocalization();

            int.TryParse(row[0], out c.id);
            
            c.Title_EN = row[1];
            c.Description_EN = row[2];

            c.Title_DE = row[3];
            c.Description_DE = row[4];

            CardList.Add(c);
        }

        return CardList;
    }
}
