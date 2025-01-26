using UnityEngine;

public class BubbleCodeCollector : MonoBehaviour
{
    public AudioSource collectionSound;
    public GameObject parent; // Assign parent object to destroy

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.name.Contains("XR Origin"))
        {
            if (collectionSound && collectionSound.clip)
            {
                AudioSource.PlayClipAtPoint(collectionSound.clip, transform.position);
            }
            Destroy(parent ? parent : gameObject);
        }
    }
}
