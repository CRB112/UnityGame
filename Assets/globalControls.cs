using UnityEngine;

public class globalControls : MonoBehaviour
{
    public Pinball_Controls controls;
    void Start()
    {

    }

    void Awake()
    {
        controls = new Pinball_Controls();
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {
        if (controls != null)
        {
            controls.Player.Disable(); // stop all events and prevent leaks
        }
    }

}
