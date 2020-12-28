using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObjects/new Action Card")]
public class Card : ScriptableObject
{

    public Sprite CardSprite;
    public Color Color;
    public int ID;
}
