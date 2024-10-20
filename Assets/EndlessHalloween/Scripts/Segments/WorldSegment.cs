using UnityEngine;

public class WorldSegment : MonoBehaviour
{
    [SerializeField] private bool isActive = false;

    private void Awake()
    {
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }
}
