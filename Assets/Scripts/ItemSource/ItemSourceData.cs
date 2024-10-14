using UnityEngine;

[CreateAssetMenu(fileName = "ItemSource", menuName = "GameData/ItemSource")]
public class ItemSourceData : ScriptableObject
{
    [SerializeField] public GameObject _stage1, _stage2, _stage3, _stage4;
    [SerializeField] public int MaxValue;
    [SerializeField] public Item _item;

}
