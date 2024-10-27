using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Player : MonoBehaviour
{
    [SerializeField] private float _attackMaxTimeout = 1f;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Animator _animator;

    public Inventory Inventory => _inventory;

    [SerializeField] private float _attackTimeout = 0f;

    private void Update()
    {
        if (_attackTimeout > 0f) _attackTimeout = Mathf.Clamp(_attackTimeout - Time.deltaTime,0, _attackMaxTimeout);
        if (_attackTimeout < 0f)
        {
            _attackTimeout = 0f;
        }
        if (_attackTimeout == 0f && _animator.GetBool("IsAttack"))
        {
            _animator.SetBool("IsAttack", false);
        }
        
        if (_attackTimeout == 0f)
        {   
            Collider[] _hitColliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1f);
            foreach (Collider hit in _hitColliders)
            {
                if (hit.TryGetComponent(out ItemSourceView source))
                {
                    int damage = 1;

                    if (source != null)
                    {
                        damage = source.GetDamage(damage, transform);
                        
                        if (damage > 0)
                        {
                            _attackTimeout = _attackMaxTimeout;
                            _animator.SetBool("IsAttack", true);
                            _inventory.ChangeItemAmount(source.Resource.type, damage);
                        }
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
