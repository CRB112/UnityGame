using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Window
{
    public string type;
    public List<GameObject> windows;
}

public class GameStateMan : MonoBehaviour
{
    private BoardMan boardMan;
    
    public List<Window> windows;
    public GameObject playBoard;

    public GameObject currentOpen;
    public GameObject currentInstance;

    private string pendingState;
    private bool isSwapping = false;

    public int round = -1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        playBoard = getWindow("Board").windows[0]; // Default board
        swapState("Start");
    }

    private void OnDestroy()
    {
    }
    public void swapState(string state, string scene = "")
    {
        if (isSwapping)
            return;
        isSwapping = true;
        if (scene != "")
        {
            pendingState = state;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(scene);
            return;
        }

        // No scene load needed → open immediately
        OpenState(state);
        isSwapping = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Wait one frame to ensure all Start() calls finished
        StartCoroutine(DelayedOpen());
    }

    private System.Collections.IEnumerator DelayedOpen()
    {
        yield return null;
        OpenState(pendingState);
        isSwapping = false;
    }

    private void OpenState(string state)
    {
        // Destroy previous window/board
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }

        if (state == "Board")
        {
            currentOpen = playBoard;
            currentInstance = Instantiate(currentOpen);

            boardMan = currentInstance.GetComponentInChildren<BoardMan>();
            FindAnyObjectByType<StatManager>().buildBTN.SetActive(true);
            boardMan.startRound();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainGame")
            {
                FindAnyObjectByType<StatManager>().buildBTN.SetActive(false);
            }
            List<GameObject> w = getWindow(state).windows;
            currentOpen = w[Random.Range(0, w.Count)];
            currentInstance = Instantiate(currentOpen);
        }
    }

    public Window getWindow(string type)
    {
        return windows.Find(w => w.type == type);
    }

    public void nextRound()
    {
        ++round;
        if (round % 4 == 0)
        {
            swapState("Shop");
        }
        else
        {
            if (currentOpen != playBoard)
            {
                swapState("Board");
            }
            else
            {
                boardMan.startRound();
            }
        }
    }
}