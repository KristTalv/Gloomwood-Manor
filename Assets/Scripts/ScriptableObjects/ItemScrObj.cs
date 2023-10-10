using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;


[CreateAssetMenu(fileName = "Item_ScrptOjc", menuName = "Item_ScrObj", order = 2)]
public class ItemScrObj : ScriptableObject
{
    public string itemName;
    [SerializeField, TextArea]
    public string itemText;
    public Sprite inventoryIcon;
    public Texture2D cursorSprite;
    public string pairObject;

    
}
