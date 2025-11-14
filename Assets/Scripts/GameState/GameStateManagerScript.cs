using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStateManagerScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject hud;
    public bool interactable = true;
    public bool cameratf = true;
    public GameObject cameraObject;
    public Camera camera;
    public static GameStateManagerScript instance;
    public static bool isPaused;
    public Vector3 savedCameraAngle;
    public Quaternion savedRotation;
    public float playerx;
    public float playery;
    public float playerz;
    public bool savedState = false;
    public GameObject player;
    public Camera pausecamera;
    public EventSystem pauseevent;
    public GameObject pausemenu2;
    public GameObject UIOverlays;
    public GameObject GameOverMenu;
    public string CurrentScene2D;
    public GameObject settingspanel;

    public bool tutorialclear = false;
    public bool l1introclear = false;
    public bool Exit2Dworld = false;
    public bool cleanerbot = false;
    public bool cleanerbotcutscene = false;
    public bool RWPCAccess = false;
    public bool L1CatBoss = false;

    private enum GameState
    {
        MainMenu, PauseMenu, Game3D, Game2D, GameOver
    }
    private static GameState state, prevstate;

    public void setstate2D()
    { state = GameState.Game2D; }
    public void setstate3D() { state = GameState.Game3D; }

    public void setstatemenu() { state = GameState.MainMenu; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(pauseMenu);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(UIOverlays);
        DontDestroyOnLoad(pausecamera);
        instance = this;
        isPaused = false;
        state = GameState.Game3D;
        if (!isPaused) pauseMenu.SetActive(false);
        if (state == GameState.MainMenu) hud.SetActive(true);

    }

    public void mainmenusetting()
    {
        if (state ==GameState.MainMenu)
        {
            settingspanel.SetActive(false);
        }
        else
        {
            pausemenu2.SetActive(true);
            settingspanel.SetActive(false);
        }
    }


    public void setState(string sstate)
    {
        if (sstate == "Game3D")
        {
            state = GameState.Game3D;
        }

    }
    public void setHud(bool a)
    { hud.SetActive(a); }

    // Update is called once per frame
    void Update()
    {

        //if (EventSystem.current != null)
            //Debug.Log(EventSystem.current.currentSelectedGameObject);

        if (!isPaused)
        {
            player = GameObject.FindWithTag("Player");
            cameraObject = GameObject.FindWithTag("MainCamera");
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if (state == GameState.MainMenu)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
        Debug.Log(prevstate);
    }

    public bool getcamera()
    {
        return cameratf;
    }

    public void setcamera(bool value)
    {
        cameratf = value;
    }

    public bool getinteract()
    { return interactable; }
    public void setinteract(bool value) { interactable = value; }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
        player.SetActive(false);
        pausecamera.gameObject.SetActive(true);
        pauseevent.gameObject.SetActive(true);
        pausemenu2.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        if (state == GameState.Game3D) { hud.SetActive(true); }
        pausecamera.gameObject.SetActive(false);
        player.SetActive(true);
        pauseevent.gameObject.SetActive(false);
        pausemenu2.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
        interactable = true;
    }

    public bool GetIsPaused() { return isPaused; }

    public void LoadScene3D(string scene)
    {

        state = GameState.Game3D;
        prevstate = GameState.Game3D;
        hud.SetActive (true);
        if (scene != "")
        {
            SceneManager.LoadScene(scene);

        }
    }
    public void LoadScene2D(string scene)
    {
        CurrentScene2D = scene;
        state = GameState.Game2D;
        prevstate = GameState.Game2D;
        if (scene != "")
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }
    public void setcoords()
    {
        Vector3 pos = player.transform.position;
        //savedRotation = camera.transform.localRotation;
        playerx = pos.x;
        playery = pos.y;
        playerz = pos.z;
        setstate(true);
    }
    public void setstate(bool a)
    {
        savedState = a;
    }

    public void tp()
    {
        if (savedState)
        {
            player.transform.position = new Vector3(playerx, playery, playerz);
            //camera.transform.localRotation = savedRotation;
            Debug.Log("Player position loaded: " + player.transform.position + savedState);
            savedState = false;
        }

    }

    public void gameOver()
    {
        //UIOverlays.SetActive(true);
        pauseMenu.SetActive(true);
        pausemenu2.SetActive(false);
        pauseevent.gameObject.SetActive(true);
        GameOverMenu.SetActive(true);
        hud.SetActive(false);
        player.SetActive(false);
        pausecamera.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
        if (state == GameState.Game3D) prevstate = GameState.Game3D;
        if (state == GameState.Game2D) prevstate = GameState.Game2D;

        state = GameState.GameOver;
        savedState = true;
    }

    public void gameOverResume()
    {

        GameOverMenu.SetActive(false);
        pauseMenu.SetActive(true);
        pausemenu2.SetActive(false);

        pauseevent.gameObject.SetActive(false);
        pausecamera.gameObject.SetActive(false);

        if (prevstate == GameState.Game3D)
        {
            player.SetActive(true);
            player.transform.position = new Vector3(playerx, playery, playerz);
            hud.SetActive(true);
            state = GameState.Game3D;
        }
        if (prevstate == GameState.Game2D)
        {
            SceneManager.LoadScene("Level 2");
            state = GameState.Game3D;
            prevstate = GameState.Game3D;
            hud.SetActive(true);
            Exit2Dworld = true;
            savedState = true;
        }


        //pausemenu2.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
        interactable = true;

    }
}
