using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMechanism : MonoBehaviour
{
    public globalControls controls;
    public BoardMan boardMan;
    public Rigidbody2D boomStick;

    [Header("Ball")]
    public List<Ball> balls;
    public GameObject nextBallPrefab;
    public GameObject nextBall;
    public float shotSpeed = 10;

    [Header("StickSpeed")]
    private Quaternion startRotation;
    private float angle = 45;
    private float speed = 5;


    void Awake()
    {
        controls = FindFirstObjectByType<globalControls>();
        boardMan = FindFirstObjectByType<BoardMan>();
    }

    void Start()
    {
        startRotation = transform.rotation;
        controls.controls.Player.BoomStick.performed += ctx =>
        {
            Fire();
        };
    }
    void Update()
    {
        float wave = Mathf.Sin(Time.time * speed) * angle;
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, wave);
    }
    public void loadAllBalls(List<Ball> d)
    {
        balls = new List<Ball>(d);
    }
    public void loadSingleBall()
    {
        if (balls.Count > 0)
        {
            GameObject newBallObj = balls[balls.Count - 1].gameObject;
            nextBallPrefab = newBallObj;
            balls.RemoveAt(balls.Count - 1);
        }
        else
        {
            balls.Clear();
            nextBallPrefab = null;
        }
    }


    public void Fire()
    {
        if (nextBallPrefab != null)
        {
            GameObject temp = Instantiate(nextBallPrefab);
            temp.transform.position = gameObject.transform.position;
            temp.GetComponent<Rigidbody2D>().linearVelocity = -boomStick.gameObject.transform.up * shotSpeed;
            boardMan.addBall(temp);

            loadSingleBall();
        }
    }

}
