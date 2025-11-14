using UnityEngine;
using TMPro;
using System.Collections;

public class HudController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static HudController instance;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        instance = this;
        DisableInteractionText();
        DisableDialogueText();
        DisableSubtextText();
        DisableLeftTitleText();
        EnableLeftTitleText(inittext);
    }
    [SerializeField] GameObject boxleft;
    [SerializeField] GameObject boxright;
    [SerializeField] GameObject boxtop;
    [SerializeField] GameObject boxbottom;
    [SerializeField] GameObject dialoguebox;
    
    
    [SerializeField] TMP_Text boxtext;
    [SerializeField] TMP_Text interactionText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text LeftTitleText;
    [SerializeField] GameObject LeftDetailText;
    [SerializeField] TMP_Text ObjectiveSubtext1;
    [SerializeField] TMP_Text ObjectiveSubtext2;
    [SerializeField] TMP_Text ObjectiveSubtext3;
    [SerializeField] TMP_Text ObjectiveSubtext4;
    [SerializeField] TMP_Text caughtText;
    public string inittext;
    public float duration = 1.0f;   // How long the effect lasts
    public float scrambleSpeed = 0.05f; // How fast it updates

    public void EnableInteractionText(string key, string text)
    {
        interactionText.text = text;
        boxtext.text = key;
        boxleft.gameObject.SetActive(true);
        boxright.gameObject.SetActive(true);
        boxtop.gameObject.SetActive(true);
        boxbottom.gameObject.SetActive(true);
        boxtext.gameObject.SetActive(true);
        
        interactionText.gameObject.SetActive(true);
        HudController.instance.DisableDialogueText();
    }

    public void EnableCaughtText(string text) {
        Debug.Log("Player Caught");
        caughtText.text = text;
        caughtText.gameObject.SetActive(true);
        StartCoroutine("DisableCaughtTextS", 2);
    }
    public void DisableCaughtText() {
        caughtText.gameObject.SetActive(false);
    }
    IEnumerator DisableCaughtTextS(int a)
    {
        yield return new WaitForSeconds(a);
        DisableCaughtText();
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
        boxleft.gameObject.SetActive(false);
        boxright.gameObject.SetActive(false);
        boxtop.gameObject.SetActive(false);
        boxbottom.gameObject.SetActive(false);
        boxtext.gameObject.SetActive(false);
    }
    public void EnableDialogueText(string text)
    {
        //dialogueText.text = text;
        StartScramble(text,dialogueText);
        dialoguebox.gameObject.SetActive(true);
        GameStateManagerScript.instance.setinteract(false);
    }
    public void EnableLeftTitleText(string text)
    {
        //LeftTitleText.text = text;
        StartScramble(text, LeftTitleText);
        LeftTitleText.gameObject.SetActive(true);
    }
    public void EnableOneSubText(string text)
    {
        StartScramble(text, ObjectiveSubtext1);
        //ObjectiveSubtext1.text = text;
        ObjectiveSubtext2.text = null;
        ObjectiveSubtext3.text = null;
        ObjectiveSubtext4.text = null;
        LeftDetailText.gameObject.SetActive(true);
    }
    public void EnableMultipleSubText(string text1, string text2, string text3, string text4)
    {
        if (text1 != "NIL")
        {
            StartScramble(text1, ObjectiveSubtext1);
            //ObjectiveSubtext1.text = text1;
        }

        if (text2 != "NIL")
        {
            StartScramble(text2, ObjectiveSubtext2);
            //ObjectiveSubtext2.text = text2;
        }
        if (text3 != "NIL")
        {
            StartScramble(text3, ObjectiveSubtext3);
            //ObjectiveSubtext3.text = text3;
        }
        if (text4 != "NIL")
        {
            StartScramble(text4, ObjectiveSubtext4);
            //ObjectiveSubtext4.text = text4;
        }
        LeftDetailText.gameObject.SetActive(true);
    }   

    public void DisableDialogueText()
    {
        dialoguebox.gameObject.SetActive(false);
        GameStateManagerScript.instance.setinteract(true);
    }

    public void DisableSubtextText()
    {
        LeftDetailText.gameObject.SetActive(false);
    }
    public void DisableLeftTitleText()
    {
        LeftTitleText.gameObject.SetActive(false);
    }

    public void DelayedDisableTutorialText(int time)
    {
        
        StartCoroutine("wipeDialogueMessage", time);

    }

    IEnumerator wipeDialogueMessage(int time)
    {
        yield return new WaitForSeconds(time);
        HudController.instance.DisableDialogueText();
        GameStateManagerScript.instance.setinteract(true);
    }

    private string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*     ";

    public void StartScramble(string a, TMP_Text tmptext)
    {
        StartCoroutine(ScrambleEffect(a, tmptext));
    }

    private IEnumerator ScrambleEffect(string targetText, TMP_Text tmpText)
    {
        int length = targetText.Length;
        char[] result = new char[length];

        // Fill with blanks initially
        for (int i = 0; i < length; i++) result[i] = ' ';

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += scrambleSpeed;
            int revealed = Mathf.FloorToInt((elapsed / duration) * length);

            for (int i = 0; i < length; i++)
            {
                if (i < revealed)
                {
                    result[i] = targetText[i]; // lock correct letter
                }
                else
                {
                    result[i] = characters[Random.Range(0, characters.Length)];
                }
            }

            tmpText.text = new string(result);
            yield return new WaitForSeconds(scrambleSpeed);
        }

        // Ensure final correct text
        tmpText.text = targetText;
    }
}

