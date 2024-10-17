using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequireditemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _amountText;

    private int _value = 0;

    public void Init(Sprite icon, int amount)
    {
        _image.sprite = icon;
        ChangeValue(amount);
    }

    public void ChangeValue(int dif)
    {
        _value += dif;
        _amountText.text = _value.ToString();
    }
}
