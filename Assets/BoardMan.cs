using System.Collections.Generic;
using UnityEngine;

public class BoardMan : MonoBehaviour
{
    public FiringMechanism boomstick;
    private globalControls controls;
    private List<GameObject> activeBalls;
    public StatManager statMan;
    private GameStateMan gameStateMan;

    private bool building = false;
    public GameObject builder;
    void Awake()
    {
        Debug.Log("BoardMan instance: " + gameObject.name + " | ID: " + GetInstanceID());
        gameStateMan = FindFirstObjectByType<GameStateMan>();
        statMan = FindFirstObjectByType<StatManager>();
        boomstick = FindFirstObjectByType<FiringMechanism>();
        controls = FindFirstObjectByType<globalControls>();
        controls.controls.Player.LeftFlipper.Enable();
        controls.controls.Player.RightFlipper.Enable();
        controls.controls.Player.BoomStick.Enable();
        controls.controls.Player.SwapToBuild.Enable();
        activeBalls = new List<GameObject>();   
        

        controls.controls.Player.SwapToBuild.performed += ctx =>
        {
            if (activeBalls.Count == 0)
                swapMode();
        };
    }
    void Update()
    {

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

    public void swapMode()
    {
        if (builder == null)
            Debug.Log("ASD");

        building = !building;
        if (building)
        {
            builder.SetActive(true);
            controls.controls.Player.LeftFlipper.Disable();
            controls.controls.Player.RightFlipper.Disable();
            controls.controls.Player.BoomStick.Disable();

            controls.controls.Player.BuildLClick.Enable();
            controls.controls.Player.BuildRClick.Enable();
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
}
