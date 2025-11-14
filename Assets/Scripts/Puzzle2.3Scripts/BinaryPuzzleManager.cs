using UnityEngine;
using TMPro;

public class BinaryPuzzleManager : MonoBehaviour
{
    public TMP_Text[] blanks;
    public string correctCode = "100100100000110";
    public GameObject exitDoor;
    public GameObject totems;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void setValue(int index, int value)
    {
        if (index >= 0 && index < blanks.Length)
        {
            blanks[index].text = value.ToString();
            blanks[index].ForceMeshUpdate();
            checkSolved();
        }
    }

    private void checkSolved()
    {
        string playerCode = "";

        foreach (TMP_Text t in blanks)
        {
            if (t == null || string.IsNullOrEmpty(t.text))
            {
                return;
            }

            playerCode += t.text;
        }

        Debug.Log("current code is " + playerCode);

        if (playerCode == correctCode)
        {
            // puzzle solved, trigger end sequence
            Debug.Log("correct code activated!");
            exitDoor.SetActive(true);
            totems.SetActive(false);
            audioManager.PlaySFX(audioManager.taskComplete);
        }
    }
    
}
