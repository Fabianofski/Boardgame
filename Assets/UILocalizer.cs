using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILocalizer : MonoBehaviour
{
    
    [System.Serializable]
    public class LanguageSprites
    {
        public string Language;
        public Sprite sprite;
    }

    public enum UITypes { Image, SpriteRenderer, Text }
    public UITypes UIType;

    [Header("Image & SpriteRenderer")]
    [SerializeField]
    public LanguageSprites[] Localization;

    [Header("Image")]
    public Image image;

    [Header("SpriteRenderer")]
    public SpriteRenderer spriteRenderer;

    [Header("Text")]
    public string id;
    public string TSVFilename;
    public TextMeshProUGUI text;
    List<TextLocalization> textLocalizations;

    void OnEnable()
    {
        if (UIType == UITypes.Text)
            textLocalizations = LoadInTSVtoText(TSVFilename);
        SwitchLanguage();
    }
    public void SwitchLanguage()
    {
        switch (UIType)
        {
            case UITypes.Image: SwitchImage();
                break;

            case UITypes.SpriteRenderer:
                SwitchSpriteRenderer();
                break;

            case UITypes.Text:
                SwitchText();
                break;
        }
    }

    void SwitchImage()
    {
        image.sprite = Localization[PlayerPrefs.GetInt("languageValue")].sprite;
        image.SetNativeSize();
    }

    void SwitchSpriteRenderer()
    {
        spriteRenderer.sprite = Localization[PlayerPrefs.GetInt("languageValue")].sprite;
    }

    void SwitchText()
    {
        TextLocalization textL = new TextLocalization();
        foreach(TextLocalization t in textLocalizations)
        {
            if(t.id == id)
            {
                textL = t;
            }
        }

        text.text = (string)textL.GetType().GetField(PlayerPrefs.GetString("language")).GetValue(textL);
    }

    List<TextLocalization> LoadInTSVtoText(string filename)
    {
        List<TextLocalization> TextList = new List<TextLocalization>();
        TextAsset LocalizationText = Resources.Load<TextAsset>(filename);

        string[] data = LocalizationText.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { '\t' });

            TextLocalization t = new TextLocalization();

            t.id = row[0];

            t.EN = row[1];

            t.DE = row[2];

            TextList.Add(t);
        }

        Debug.Log("Loaded List");
        return TextList;
    }

}
