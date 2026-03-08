using System.Collections.Generic;
using UnityEngine;

public class ObjSeller : MonoBehaviour
{
    public GameObject parent;

    public StatManager statMan;
    public List<DynamicObject> objs = new List<DynamicObject>();
    void OnEnable()
    {
        statMan = FindAnyObjectByType<StatManager>();
        objs.Clear();

    }
    void Update()
    {

    }
    public void sell(DynamicObject d) {
        statMan.addCash(d.getCost() / 2);
    }
}
