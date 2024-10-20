using System;
using UnityEngine;

public class ItemSourceView : MonoBehaviour
{
    [SerializeField] private ItemSourceData _data;
    [SerializeField] private GameObject _object;
    [SerializeField] [Range(0, 100)] private int _value;
    [SerializeField] private Item _resource;
    [SerializeField] private float _maxRespawnTimeout = 5f;
    [SerializeField] private float _respawnTimeout;
    [SerializeField] private bool _hideWhenZero;
    
    private GameObject _stage1, _stage2, _stage3, _stage4;

    public Item Resource => _resource;
    public int Value => _value;


    private void Awake()
    {
        if (_stage1 == null) _stage1 = Instantiate(_data._stage1, transform);
        if (_stage2 == null) _stage2 = Instantiate(_data._stage2, transform);
        if (_stage3 == null) _stage3 = Instantiate(_data._stage3, transform);
        if (_stage4 == null) _stage4 = Instantiate(_data._stage4, transform);

        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);
        _stage4.SetActive(false);


        _value = _data.MaxValue;
        _respawnTimeout = 0f;
    }

    private void Update()
    {
        if (_value > _data.MaxValue / 3f * 2) _object = _stage1;
        else if (_value >= _data.MaxValue / 3f) _object = _stage2;
        else if (_value > 0f) _object = _stage3;
        else if (_value <= 0 && _respawnTimeout == 0f)
        {
            _object = _stage4;
            _respawnTimeout = _maxRespawnTimeout;
        }
        
        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);
        _stage4.SetActive(false);
        _object.SetActive(!(_value == 0 && _hideWhenZero));
        
        if (_respawnTimeout > 0f)
        {
            _respawnTimeout -= Time.deltaTime;
            if (_respawnTimeout < 0f) _respawnTimeout = 0f;
        }

        if (_respawnTimeout == 0f && _value == 0)
        {
            _value = _data.MaxValue;
            _object.SetActive(true);
        }
    }

    public int GetDamage(int damage, Transform effectTransform)
    {
        if (damage <= 0) return 0;
        if (_value == 0) return 0;
        
        if (effectTransform == null)
        {
            effectTransform = transform;
        }

        if (damage >= _value)
        {
            damage = _value;
            _value = 0;
        } else
        {
            _value -= damage;
            Instantiate(_resource.effect, effectTransform);
        }
        return damage;

    }
}
