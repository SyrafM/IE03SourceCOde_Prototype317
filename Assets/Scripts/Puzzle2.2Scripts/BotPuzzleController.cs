using UnityEngine;
using UnityEngine.UI;

public class BotPuzzleController : MonoBehaviour
{
    // int progressAmount;
    public Slider progressSlider;
    public GameObject ExitDoor;
    public GameObject Timer;

    private int maxBots;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // progressAmount = 0;
        
    }

    public void SetMaxBots(int max)
    {
        maxBots = max;
        progressSlider.maxValue = max;
        progressSlider.value = 0;
    }

    public void UpdateProgress(int botsKilled)
    {
        progressSlider.value = botsKilled;
        if (progressSlider.value >= progressSlider.maxValue)
        {
            Debug.Log("All bots killed!");
            ExitDoor.SetActive(true);
            Timer.SetActive(false);
            audioManager.PlaySFX(audioManager.taskComplete);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
