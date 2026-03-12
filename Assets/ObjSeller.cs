using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ObjSeller : MonoBehaviour
{
    public GameObject ballParent;
    public GameObject pinParent;
    public GameObject itemBox;
    private List<GameObject> boxes = new List<GameObject>();
    public Button sellB;

    public StatManager statMan;
    void Start()
    {
    }
    void OnEnable()
    {
        statMan = FindAnyObjectByType<StatManager>();
        listItems();
    }
    void Update()
    {

    }
    public bool sell(DynamicObject d)
    {
        statMan.addCash(d.getCost() / 2);
        if (d is Ball && statMan.balls.Count == 1)
            return false;
        else
            statMan.removeItem(d);
        return true;
    }
    public void listItems()
    {
        clearItems();
        foreach (DynamicObject d in statMan.balls.Cast<DynamicObject>().Concat(statMan.pins))
        {
            GameObject i = Instantiate(itemBox, ballParent.transform.position, Quaternion.identity, d is Ball ? ballParent.transform : pinParent.transform);
            boxes.Add(i);
            i.AddComponent<itemSlot>();
            i.GetComponent<itemSlot>().Init(d, false);
            i.GetComponent<ItemUI>().dO = d;
            i.GetComponent<Button>().onClick.AddListener(() =>
            {
                sell(d);
                Destroy(i);
            });
        }
    }
    public void clearItems()
    {
        foreach (GameObject g in boxes)
            Destroy(g);
        boxes.Clear();
    }
}
