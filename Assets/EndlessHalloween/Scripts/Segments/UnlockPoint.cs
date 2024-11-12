using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPoint : MonoBehaviour
{
    [SerializeField] private WorldSegment _unlokingSegment;
    [SerializeField] private RequireditemView _slotPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GridLayoutGroup _container;
    [SerializeField] private Player _player;
    [SerializeField] List<UnlockSlot> _slots = new();

    [SerializeField] private List<RequireditemView> _requirements = new();

    private void Start()
    {
        foreach (UnlockSlot slot in _slots)
        {
            Debug.Log($"{slot._item.name}, {slot.Amount}");
            RequireditemView _inventorySlot = Instantiate(_slotPrefab, _container.transform);
            _inventorySlot.Init(slot._item, slot._item.icon, slot.Amount);

            _requirements.Add(_inventorySlot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_unlokingSegment) return;

        if (other.TryGetComponent(out Player player))
        {
            if (CanUnlock())
            {
                UnlockSegment();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_unlokingSegment) return;

        if (other.TryGetComponent(out Player player))
        {
            if (CanUnlock())
            {
                UnlockSegment();
            } else
            {
                foreach (RequireditemView slot in _requirements)
                {
                    if (slot.Value > 0)
                    {
                        var playerInventory = _player.Inventory;
                        int haveCount = playerInventory.GetItemAmount(slot.Item.type);
                        Debug.Log($"playr has {haveCount} items");
                        if (haveCount > 0)
                        {
                            playerInventory.ChangeItemAmount(slot.Item.type, -1);
                            slot.ChangeValue(-1);
                        }
                    }
                }
            }
        }
    }

    public bool CanUnlock()
    {
        foreach (RequireditemView slot in _requirements)
        {
            Debug.Log($"required {slot.Value}");
            if (slot.Value > 0) return false;
        }
        return true;
    }

    public void UnlockSegment()
    {
        _unlokingSegment.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_player != null)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance <= 5)
            {
                _canvas.gameObject.SetActive(true);
            }
            else
            {
                _canvas.gameObject.SetActive(false);
            }

            if (!_canvas.gameObject.activeInHierarchy) return;

            Quaternion rawRoation = Quaternion.Slerp(_canvas.transform.rotation,
                                                     Quaternion.LookRotation(_canvas.transform.position - _player.transform.position),
                                                     2 * Time.deltaTime);
            _canvas.transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
        }
    }
}
