using UnityEngine;

public class CollectionDetector : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.gameObject.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            audioManager.PlaySFX(audioManager.collectItem);
        }
    }
}
