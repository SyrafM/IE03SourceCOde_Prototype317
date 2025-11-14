using UnityEngine;
using UnityEngine.SceneManagement;

public class TpOnVideoEnd : MonoBehaviour
{
    public void OnCutSceneEnd()
    {
        Debug.Log("End");
        SceneManager.LoadScene("MainMenu");
    }
    }
