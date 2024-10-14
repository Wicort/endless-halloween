using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _amountText;
    
    [SerializeField] public Item _item;
    [SerializeField] public int Amount;

    public void Init(Item item, int value)
    {
        _item = item;
        Amount = value;

        _itemImage.sprite = _item.icon;
        _amountText.text = Amount.ToString();        
    }

    public void RefreshSlot()
    {
        _amountText.text = Amount.ToString();
    }
}
