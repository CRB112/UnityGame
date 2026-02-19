using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class StickControls : MonoBehaviour
{
    public globalControls controls;

    private List<GameObject> Lsticks;
    private List<GameObject> Rsticks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls = FindFirstObjectByType<globalControls>();

        Lsticks = new List<GameObject>(GameObject.FindGameObjectsWithTag("LStick"));
        Rsticks = new List<GameObject>(GameObject.FindGameObjectsWithTag("RStick"));

        controls.controls.Player.LeftFlipper.performed += ctx => flickSticks(Lsticks, true);
        controls.controls.Player.LeftFlipper.canceled   += ctx => flickSticks(Lsticks, false);

        // Right button events
        controls.controls.Player.RightFlipper.performed += ctx => flickSticks(Rsticks, true);
        controls.controls.Player.RightFlipper.canceled   += ctx => flickSticks(Rsticks, false);
    }
    private void Awake()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void flickSticks(List<GameObject> l, bool b = true)
    {
        l.RemoveAll(x => x == null);
        foreach (GameObject s in l)
            s.GetComponent<Stick>().flick(b);
    } 
}
