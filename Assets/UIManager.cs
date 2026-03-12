using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIElement {
    public string name;
    public GameObject obj;
}


public class UIManager : MonoBehaviour
{
    public List<UIElement> elementList = new List<UIElement>();
    public Dictionary<string, GameObject> elements = new Dictionary<string, GameObject>();
    private GameObject active;

    private globalControls controls;
    void Start()
    {
        controls = FindAnyObjectByType<globalControls>();
        foreach (UIElement u in elementList)
        {
            elements[u.name] = u.obj;
        }
        controls.controls.Player.MenuESC.performed += ctx =>
        {
            if (active == null)
                open("Start");
            else
                close();
        };
    }

    void Update()
    {
        
    }
    public void open(string name)
    {
        if (active != null && name != active.name)
            active.SetActive(false);
        if (elements.ContainsKey(name))
            {
                elements[name].SetActive(true);
                active = elements[name];
            }
    }
    public void close() {
        active.SetActive(false);
    }
}
