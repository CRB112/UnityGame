using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{
    public GameObject emptySlot;
    public DynamicObject obj;
    //Bool is purchase or sell - true for purchase, false for sell
    public void Init(DynamicObject ob, bool pos)
    {
        emptySlot = FindAnyObjectByType<SystemSerializer>().findPrefab("ItemBox_Purchased");
        Debug.Log(emptySlot);
        obj = ob;
        UnityEngine.UI.Image[] imgs = GetComponentsInChildren<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image i in imgs)
        {
            if (i.gameObject != gameObject)
            {
                i.sprite = obj.gameObject.GetComponent<SpriteRenderer>().sprite;
            }
            else if (pos)
            {
                GetComponent<UnityEngine.UI.Image>().color = Rarity.GetColor(obj.rarity);
            }
        }
        GetComponentInChildren<TextMeshProUGUI>().text = pos ? Rarity.GetCost(obj.rarity).ToString() : (Rarity.GetCost(obj.rarity) / 2).ToString();
    }
    //Returns bool for disableBox() semantics
    public bool purchase()
    {
        StatManager statMan = GameObject.FindAnyObjectByType<StatManager>();
        if (statMan.cash >= obj.getCost())
        {
            statMan.addCash(-obj.getCost());
            statMan.addItem(obj);
            return true;
        }
        else
        {
            return false;
            //Animation
        }
    }
    public void disableBox()
    {
        int index = gameObject.transform.GetSiblingIndex();
        GameObject temp = Instantiate(emptySlot, transform.parent.transform);
        temp.transform.SetSiblingIndex(index);
        Destroy(gameObject);
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
            temp.GetComponent<ItemUI>().dO = o;
            islot.Init(o, true);
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                bool b = islot.purchase();
                if (b)
                {
                    islot.disableBox();
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
                Debug.Log(rarity);
                return rarity;
            }
        }
        return weights.Length;
    }
    private void rollItems()
    {
        for (int i = 0; i < itemCount; ++i)
        {
            DynamicObject item = null;
            int rarity = rollRarity(statMan.luck);
            do
            {
                Dictionary<int, List<DynamicObject>> ls = Random.Range(0, 2) == 1 ?
                    ss.ALLBALLS :
                    ss.ALLPINS;
                if (ls.ContainsKey(rarity))
                {
                    item = ls[rarity][Random.Range(0, ls[rarity].Count - 1)];
                }
            }
            while (item == null);
            items.Add(item);
        }
    }
}
