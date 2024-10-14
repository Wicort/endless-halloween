using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPoint : MonoBehaviour
{
    [SerializeField] private WorldSegment _unlokingSegment;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Is trigger");
        if (!_unlokingSegment) return;

        if (other.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Trying to unlock");
            if (CanUnlock())
            {
                Debug.Log("Unlocked");
                UnlockSegment();
            }
        }
    }

    public bool CanUnlock()
    {
        Debug.Log("Can unlock");
        return true;
    }

    public void UnlockSegment()
    {
        _unlokingSegment.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
