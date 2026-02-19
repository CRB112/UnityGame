using UnityEngine;

public class BallLose : MonoBehaviour
{
    public BoardMan board;
    void Start()
    {
        board = GameObject.FindFirstObjectByType<BoardMan>();
    }


    void Update()
    {

    }
    void OnTriggerStay2D(UnityEngine.Collider2D col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            board.returnBall(ball.gameObject);
        }
    }
}
