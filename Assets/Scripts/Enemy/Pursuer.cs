using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] private float _pursuingSpeed = 0.5f;
    
    private Enemy _self;
    private ItemSourceView _sourceView;

    private void Awake()
    {
        _self = gameObject.GetComponent<Enemy>();
        _sourceView = gameObject.GetComponent<ItemSourceView>();
    }

    private void Update()
    {
        if (_sourceView.Value == 0) return;

        Collider[] _hitColliders = Physics.OverlapSphere(transform.position, _self._radius, LayerMask.GetMask("Player"));
        foreach (Collider hit in _hitColliders)
        {
            Debug.Log("Argh!!");
            var _player = hit.GetComponent<Player>();

            Vector3 moveDirection = transform.forward * _pursuingSpeed * Time.deltaTime;

            _characterController.Move(moveDirection);
        }
    }
}
