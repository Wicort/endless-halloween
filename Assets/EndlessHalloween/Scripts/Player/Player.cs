using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _attackMaxTimeout = 1f;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Animator _animator;

    public Inventory Inventory => _inventory;

    private float _attackTimeout = 0f;

    private void Update()
    {
        if (_attackTimeout > 0f) _attackTimeout = Mathf.Clamp(_attackTimeout - Time.deltaTime,0, _attackMaxTimeout);
        _animator.SetBool("IsAttack", false);
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
                    if (damage > 0)
                    {
                        _animator.SetBool("IsAttack", true);
                        _inventory.ChangeItemAmount(source.Resource.type, damage);
                    }

                    
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
