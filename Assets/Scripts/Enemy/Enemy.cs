using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float _radius = 6f;
    private void Update()
    {
        Collider[] _hitColliders = Physics.OverlapSphere(transform.position, _radius, LayerMask.GetMask("Player"));
        foreach (Collider hit in _hitColliders)
        {
            Debug.Log("Argh!!");
            var _player = hit.GetComponent<Player>();
            Quaternion rawRoation = Quaternion.Slerp(transform.rotation,
                                                     Quaternion.LookRotation(_player.transform.position - transform.position),
                                                     2 * Time.deltaTime);
            transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
