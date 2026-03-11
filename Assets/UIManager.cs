using System;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        foreach (UIElement u in elementList) {
            elements[u.name] = u.obj;
        }
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
    public void close(string screen) {
        active.SetActive(false);
    }
}
