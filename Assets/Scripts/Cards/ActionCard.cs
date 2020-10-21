using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

    void Start()
    {
        cardContainer = GetComponentInParent<CardContainer>();
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

        Card card = ScriptableObject.CreateInstance("Card") as Card;

        switch (cardtype)
        {
            case CardContainer.CardType.TextCard:
                index = Random.Range(0, cardContainer.TextCards.Count);
                card = cardContainer.TextCards[index];
                cardContainer.UsedTextCards.Add(cardContainer.TextCards[index]);
                cardContainer.TextCards.RemoveAt(index);
                cardContainer.CheckIfAllCardsUsed();
                break;

            case CardContainer.CardType.SpecificCard:
                card = specificCard;
                break;
        }

        
        TitleText.text = card.Title;
        DescriptionText.text = card.Description;
        image.sprite = card.CardSprite;
        Panel.GetComponent<Image>().color = card.Color;

        button.onClick.AddListener(CardFunction.Invoke);
    }

}
