using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _container;
    [SerializeField] private InventorySlot _slotPrefab;

    [SerializeField] private List<Item> _items = new();
    [SerializeField] private List<InventorySlot> _slots = new();

    private void Awake()
    {
        foreach (Item item in _items)
        {
            InventorySlot slot = Instantiate(_slotPrefab, _container.transform);
            slot.Init(item, 5);
            _slots.Add(slot);
        }
    }

    public void ChangeItemAmount(ItemType type, int amount)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].type == type)
            {
                _slots[i].Amount += amount;
                _slots[i].RefreshSlot();
            }
        }
    }
}
