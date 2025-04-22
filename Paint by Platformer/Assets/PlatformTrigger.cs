using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private MovePillar mover;

    void Start()
    {
        mover = GetComponentInParent<MovePillar>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover.SetPlayerOnPlatform(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover.SetPlayerOnPlatform(false);
        }
    }
    
}
