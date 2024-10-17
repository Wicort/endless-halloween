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

    private void Start()
    {
        foreach (UnlockSlot slot in _slots)
        {
            Debug.Log($"{slot._item.name}, {slot.Amount}");
            var _inventorySlot = Instantiate(_slotPrefab, _container.transform);
            _inventorySlot.Init(slot._item.icon, slot.Amount);
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

    public bool CanUnlock()
    {
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
            Quaternion rawRoation = Quaternion.Slerp(_canvas.transform.rotation,
                                                                 Quaternion.LookRotation(_canvas.transform.position - _player.transform.position),
                                                                 2 * Time.deltaTime);
            _canvas.transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
        }
    }
}
