using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatManager : MonoBehaviour
{
    public int score;
    public int cash = 100;
    public float luck = 0;
    public List<Ball> balls;
    public List<Pin> pins;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI cashText;

    public GameObject pinsDisplayParent; //Used for disabling
    public GameObject pinsDisplay;
    public GameObject pinBox;

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
            displayPins();
        }
    }
    private void displayPins()
    {
        foreach (Transform child in pinsDisplay.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Pin p in pins)
        {
            GameObject temp = Instantiate(pinBox, pinsDisplay.transform);
            temp.GetComponent<Image>().sprite = p.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
