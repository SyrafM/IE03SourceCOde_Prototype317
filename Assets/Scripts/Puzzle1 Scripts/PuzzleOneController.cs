using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleOneController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    public GameObject ExitDoor;
    public GameObject Timer;
    public GameObject tileSpawner;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressAmount = 0;
        FragmentScript.OnFragmentCollect += IncreaseProgressAmount;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount >= 8)
        {
            // Level complete
            Debug.Log("All fragments collected");
            audioManager.PlaySFX(audioManager.taskComplete);
            ExitDoor.SetActive(true);
            Timer.SetActive(false);
            tileSpawner.SetActive(false);
        }
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
