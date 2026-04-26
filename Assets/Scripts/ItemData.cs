using UnityEngine;
 
[CreateAssetMenu(
    fileName = "NewItemData",
    menuName = "Game/Item Data"
)]
public class ItemData : ScriptableObject
{
    [Header("Información General")]
    public string itemName;
    [TextArea] public string description;
 
    [Header("Gameplay")]
    public int pointValue = 1;
 
    [Header("Visual")]
    public Sprite icon;
    public Color particleColor = Color.yellow;
 
    [Header("Audio")]
    public AudioClip collectSound;
}

