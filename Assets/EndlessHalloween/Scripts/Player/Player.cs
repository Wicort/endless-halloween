using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _attackMaxTimeout = 1f;
    [SerializeField] private Inventory _inventory;

    public Inventory Inventory => _inventory;

    private float _attackTimeout;

    private void Update()
    {
        if (_attackTimeout > 0f) _attackTimeout = Mathf.Clamp(_attackTimeout - Time.deltaTime,0, _attackMaxTimeout);

        if (_attackTimeout == 0f)
        {
            Collider[] _hitColliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1f);
            foreach (Collider hit in _hitColliders)
            {
                if (hit.TryGetComponent(out ItemSourceView source))
                {
                    int damage = 1;
                    damage = source.GetDamage(damage, transform);
                    _attackTimeout = _attackMaxTimeout;

                    _inventory.ChangeItemAmount(source.Resource.type, damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward + transform.up, 1f);
    }

}
