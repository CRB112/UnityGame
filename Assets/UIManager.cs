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
    void Start()
    {
        foreach (UIElement u in elementList) {
            elements[u.name] = u.obj;
        }
    }

    void Update()
    {
        
    }
    public void open(string name) {
        if (elements.ContainsKey(name))
            elements[name].SetActive(true);
    }
    public void close(string screen) {
        if (elements.ContainsKey(name))
            elements[name].SetActive(false);
    }
}
