using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TotemScript : MonoBehaviour, IInteractable
{
    public int index; // which blank this totem is linked to
    public bool isTopTotem; // 1 = true, 0 = false
    public BinaryPuzzleManager puzzleManager;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            int value = isTopTotem ? 0 : 1;
            puzzleManager.setValue(index, value);
            audioManager.PlaySFX(audioManager.click);

        }
    }
}
