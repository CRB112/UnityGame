using UnityEngine;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{
    private GameStateMan gameStateMan;
    public Button[] buttons;
    void Start()
    {
        gameStateMan = FindAnyObjectByType<GameStateMan>();
        buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        Debug.Log(buttons.Length);
        assignButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void assignButtons()
    {
        foreach (Button b in buttons)
        {
            if (b.gameObject.name == "StartBTN")
            {
                b.onClick.AddListener(() =>
                {
                    gameStateMan.swapState("Shop", "MainGame");
                });
            }
        }
    }
}
