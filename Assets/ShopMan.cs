using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{
    public DynamicObject obj;
    public void Init(DynamicObject ob)
    {
        obj = ob;
        GetComponentInChildren<TextMeshProUGUI>().text = Rarity.GetCost(obj.rarity).ToString();
        UnityEngine.UI.Image[] imgs = GetComponentsInChildren<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image i in imgs)
        {
            if (i.gameObject != gameObject)
            {
                i.sprite = obj.gameObject.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                GetComponent<UnityEngine.UI.Image>().color = Rarity.GetColor(obj.rarity);
            }
        }
    }
    //Returns bool for disableBox() semantics
    public bool purchase()
    {
        StatManager statMan = GameObject.FindAnyObjectByType<StatManager>();
        if (statMan.cash >= obj.getCost())
        {
            statMan.cash -= obj.getCost();
            statMan.addItem(obj);
            return true;
        }
        else
        {
            return false;
            //Animation
        }
    }
}
public class ShopMan : MonoBehaviour
{
    public SystemSerializer ss;
    public globalControls controls;
    public StatManager statMan;

    public GameObject fillSpace;
    public GameObject itemSlot;
    public GameObject emptySlot;
    public Button b;

    int[] weights = {
        50,
        25,
        15,
        7,
        3
    };

    public int itemCount = 6;
    public List<DynamicObject> items;


    void Start()
    {
        ss = FindAnyObjectByType<SystemSerializer>();
        statMan = FindAnyObjectByType<StatManager>();
        controls = FindAnyObjectByType<globalControls>();
        statMan.pinsDisplayParent.SetActive(false);
        controls.controls.Player.LeftFlipper.Disable();
        controls.controls.Player.RightFlipper.Disable();
        controls.controls.Player.BoomStick.Disable();
        b.onClick.AddListener(() =>
        {
            FindAnyObjectByType<GameStateMan>().nextRound();
        });

        stockShop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        statMan.pinsDisplayParent.SetActive(true);
    }
    public void stockShop()
    {
        rollItems();
        for (int i = 0; i < itemCount; ++i)
        {
            GameObject temp = Instantiate(itemSlot);
            temp.transform.SetParent(fillSpace.transform, false);
            temp.AddComponent<itemSlot>();
            itemSlot islot = temp.GetComponent<itemSlot>();
            DynamicObject o = items[i];
            islot.Init(o);
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                bool b = islot.purchase();
                if (b)
                {
                    disableBox(temp);
                }
            });
        }
    }
    public int rollRarity(float luck)
    {
        int roll = Random.Range(1, 101);
        int curr = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            curr += weights[i];

            if (roll <= curr)
            {
                int rarity = i + 1; // convert 0–4 → 1–5

                // Luck bonus roll
                int rand = Random.Range(1, 101);
                if (rand <= luck * 0.3f)
                    rarity = Mathf.Min(rarity + 1, weights.Length);

                return rarity;
            }
        }
        return weights.Length;
    }
    private void rollItems()
    {
        for (int i = 0; i < itemCount; ++i)
        {
            int rarity = rollRarity(statMan.luck);
            DynamicObject item = Random.Range(0, 2) == 1 ?
                ss.ALLBALLS[rarity][Random.Range(0, ss.ALLBALLS[rarity].Count - 1)] :
                ss.ALLPINS[rarity][Random.Range(0, ss.ALLPINS[rarity].Count - 1)];
            items.Add(item);
        }
    }
    private void disableBox(GameObject g)
    {
        int index = g.transform.GetSiblingIndex();
        Destroy(g);

        GameObject temp = Instantiate(emptySlot, fillSpace.transform);
        temp.transform.SetSiblingIndex(index);
    }
}
