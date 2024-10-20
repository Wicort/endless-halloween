using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequireditemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _amountText;

    private Item _item;
    public Item Item => _item;

    private int _value = 0;

    public int Value => _value;

    public void Init(Item item, Sprite icon, int amount)
    {
        _item = item;
        _image.sprite = icon;
        _value = 0;
        ChangeValue(amount);
    }

    public void ChangeValue(int dif)
    {
        _value += dif;
        _amountText.text = _value.ToString();
    }
}
