using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _attackMaxTimeout = 1f;
    [SerializeField] private Inventory _inventory;

    private float _attackTimeout;

    private void Update()
    {
        if (_attackTimeout > 0f) _attackTimeout = Mathf.Clamp(_attackTimeout - Time.deltaTime,0, _attackMaxTimeout);
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.TryGetComponent<ItemSourceView>(out ItemSourceView source))
        {
            if (_attackTimeout == 0f)
            {
                int damage = 3;
                damage = source.GetDamage(damage, transform);
                _attackTimeout = _attackMaxTimeout;
                
                _inventory.ChangeItemAmount(source.Resource.type , damage);
            }
        }
    }
}
