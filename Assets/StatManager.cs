using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public int score;
    public float luck = 0;
    public List<Ball> balls;
    public List<Pin> pins;

    public TextMeshProUGUI scoreText;

    void Start()
    {
    }

    void Update()
    {

    }
    public void addScore(int val)
    {
        score += val;
        scoreText.text = score.ToString();
    }

    public void addItem(DynamicObject d)
    {
        if (d is Ball ball)
        {
            balls.Add(ball);
        }
        else if (d is Pin pin)
        {
            pins.Add(pin);
        }
    }
}
