using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pursuer : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] private float _pursuingSpeed = 0.5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _maxAttackTimeout = 1f;
    [SerializeField] private float _attackDistance = 1.1f;
    [SerializeField] private GameObject _target;
    
    private Enemy _self;
    private ItemSourceView _sourceView;
    private float _attackTimeout = 0;

    private void Awake()
    {
        _self = gameObject.GetComponent<Enemy>();
        _sourceView = gameObject.GetComponent<ItemSourceView>();
        _attackTimeout = 0f;
    }

    private void Update()
    {
        if (_attackTimeout > 0f)
        {
            _attackTimeout -= Time.deltaTime;
        }
        if (_attackTimeout <= 0f)
        {
            _attackTimeout = 0f;
            _target = null;
        }

        if (_animator != null)
        {
            _animator.SetBool("IsRun", false);
            if (_attackTimeout == 0f) _animator.SetBool("IsAttack", false);
        }

        if (_sourceView.Value == 0f) return;

        Collider[] _hitColliders = Physics.OverlapSphere(transform.position, _self._radius, LayerMask.GetMask("Player"));
        foreach (Collider hit in _hitColliders)
        {
            if (_animator != null) _animator.SetBool("IsRun", true);
            
            float distance = Vector3.Distance(transform.position, hit.transform.position);
         
            if (distance <= _attackDistance && _attackTimeout == 0f)
            {
                if (_animator != null) _animator.SetBool("IsAttack", true);
                _attackTimeout = _maxAttackTimeout;
                _target = hit.gameObject;
                StartCoroutine(doDamage());
            }
            else
            {
                if (_animator != null && _attackTimeout == 0f)
                {
                    Vector3 moveDirection = transform.forward * _pursuingSpeed * Time.deltaTime;
                    _characterController.Move(moveDirection);
                    _animator.SetBool("IsRun", true);
                }
            }
        }
    }

    private IEnumerator doDamage()
    {
        yield return new WaitForSeconds(_maxAttackTimeout / 2f);

        if (_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.transform.position);
            if (distance <= _attackDistance)
            {
                Debug.Log("damage");
            }
        }
    }
}
