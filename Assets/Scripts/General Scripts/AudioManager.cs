using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip playerDeath;
    public AudioClip playerHurt;
    public AudioClip playerShoot;
    public AudioClip bossHurt;
    public AudioClip bossDeath;
    public AudioClip bossAttackOne;
    public AudioClip bossAttackTwo;
    public AudioClip bossAttackThree;
    public AudioClip taskComplete;
    public AudioClip collectItem;
    public AudioClip click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = UnityEngine.Random.Range(1f, 1.2f);
        SFXSource.PlayOneShot(clip);
    }
}
