using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "GameData/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public Resource model;
    public ParticleSystem effect;
}
