using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardMan : MonoBehaviour
{
    public FiringMechanism boomstick;
    private globalControls controls;
    private List<GameObject> activeBalls;
    public StatManager statMan;
    private GameStateMan gameStateMan;
    public GameObject buildBTN;

    public GameObject boardObjects;
    private bool building = false;
    public GameObject builder;
    void Awake()
    {
        Debug.Log("BoardMan instance: " + gameObject.name + " | ID: " + GetInstanceID());
        gameStateMan = FindFirstObjectByType<GameStateMan>();
        statMan = FindFirstObjectByType<StatManager>();
        boomstick = FindFirstObjectByType<FiringMechanism>();
        controls = FindFirstObjectByType<globalControls>();
        statMan.pinsDisplay.SetActive(true);
        controls.controls.Player.LeftFlipper.Enable();
        controls.controls.Player.RightFlipper.Enable();
        controls.controls.Player.BoomStick.Enable();
        controls.controls.Player.SwapToBuild.Enable();
        activeBalls = new List<GameObject>();


        controls.controls.Player.SwapToBuild.performed += OnSwapPerformed;
    }
    void Start()
    {
        findBuildButton();
    }
    void Update()
    {

    }
    void OnDestroy()
    {
        if (controls != null)
            controls.controls.Player.SwapToBuild.performed -= OnSwapPerformed;
    }
    public void getBalls()
    {
        foreach (Ball b in FindObjectsByType<Ball>(FindObjectsSortMode.None))
            addBall(b.gameObject);
    }
    public void returnBall(GameObject b)
    {
        activeBalls.Remove(b);
        Destroy(b);
        if (activeBalls.Count == 0 && boomstick.balls.Count == 0)
        {
            endRound();
        }
    }
    public void addBall(GameObject b)
    {
        activeBalls.Add(b);
    }
    public void startRound()
    {
        boomstick.loadAllBalls(statMan.balls);
        boomstick.loadSingleBall();
    }
    private void endRound()
    {
        gameStateMan.nextRound();
    }

    public void swapMode(Pin p = null)
    {
        building = !building;
        if (building && activeBalls.Count == 0)
        {
            builder.SetActive(true);
            controls.controls.Player.LeftFlipper.Disable();
            controls.controls.Player.RightFlipper.Disable();
            controls.controls.Player.BoomStick.Disable();

            controls.controls.Player.BuildLClick.Enable();
            controls.controls.Player.BuildRClick.Enable();
            if (p != null)
            {
                GridSystem gs = builder.GetComponentInChildren<GridSystem>();
                gs.selectBuild(p);
            }
        }
        else
        {
            builder.SetActive(false);
            controls.controls.Player.LeftFlipper.Enable();
            controls.controls.Player.RightFlipper.Enable();
            controls.controls.Player.BoomStick.Enable();

            controls.controls.Player.BuildLClick.Disable();
            controls.controls.Player.BuildRClick.Disable();
        }
    }

    private void OnSwapPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (activeBalls.Count == 0)
            swapMode();
    }
    private void findBuildButton()
    {
        Button[] btns = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach(Button b in btns) {
            if (b.gameObject.name == "BuildBTN")
            {
                buildBTN = b.gameObject;
                b.onClick.AddListener(() =>
                {
                    swapMode();
                });
            }
        }
    }
}
