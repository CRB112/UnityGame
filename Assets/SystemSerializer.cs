using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
public enum RarityLevel
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5
}

public static class Rarity
{
    private static int baseCost = 50;

    private static Color32[] rarityColors = {
    new Color32(190, 190, 190, 255), // Common (clean silver-gray)
    new Color32(120, 220, 140, 255), // Uncommon (fresh green)
    new Color32(110, 170, 255, 255), // Rare (bright soft blue)
    new Color32(190, 140, 255, 255), // Epic (vivid soft purple)
    new Color32(255, 190, 110, 255), // Legendary (bright warm gold)
    };

    public static Color GetColor(RarityLevel rarity)
    {
        return rarityColors[(int)rarity - 1];
    }

    public static int GetCost(RarityLevel rarity)
    {
        return baseCost * (int)Math.Pow(2, (int)rarity - 1);
    }
}

/*
[CreateAssetMenu(fileName = "NewObj", menuName = "Buyable")]
public class buyableItem : ScriptableObject
{

    public Rarity rarity;
    public int cost;
    public GameObject obj;

    public void Init(GameObject ob, int r)
    {
        obj = ob;
        rarity = new Rarity();
        rarity.Init(r);
        cost = rarity.getCost();
    }
}*/



public class SystemSerializer : MonoBehaviour
{
    public Dictionary<int, List<DynamicObject>> ALLBALLS = new Dictionary<int, List<DynamicObject>>();
    public Dictionary<int, List<DynamicObject>> ALLPINS = new Dictionary<int, List<DynamicObject>>();
    public Dictionary<string, List<GameObject>> ALLBOARDS = new Dictionary<string, List<GameObject>>();
    void Start()
    {
        LoadAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadAll()
    {
        loadAllBalls();
        loadAllPins();
        loadAllBoards();
    }
    private void loadAllBalls()
    {
        for (int i = 1; i <= 5; ++i)
        {
            string folderName = i.ToString();
            string path = "Balls/" + folderName;
            GameObject[] balls = Resources.LoadAll<GameObject>(path);
            if (balls.Length == 0)
                continue;
            ALLBALLS[i] = new List<DynamicObject>();
            for (int j = 0; j < balls.Length; ++j)
            {
                Ball item = balls[j].GetComponent<Ball>();
                item.Init();
                Debug.Log(item);
                ALLBALLS[i].Add(item);
            }
        }
    }
    private void loadAllPins()
    {
        for (int i = 1; i <= 5; ++i)
        {
            string folderName = i.ToString();
            string path = "Pins/" + folderName;
            GameObject[] pins = Resources.LoadAll<GameObject>(path);
            if (pins.Length == 0)
                continue;
            ALLPINS[i] = new List<DynamicObject>();
            for (int j = 0; j < pins.Length; ++j)
            {
                Pin item = pins[j].GetComponent<Pin>();
                item.Init();
                ALLPINS[i].Add(item);
            }
        }
    }
    private void loadAllBoards()
    {
        string[] types = { "Boards", "Shops", "Start" };
        for (int i = 0; i < types.Length - 2; ++i)
        {
            string path = "Boards/" + types[i];
            GameObject[] boards = Resources.LoadAll<GameObject>(path);
            if (boards.Length == 0)
                continue;
            ALLBOARDS[types[i]] = new List<GameObject>(boards);
        }
        ALLBOARDS["Start"] = new List<GameObject>(Resources.LoadAll<GameObject>("Baords/Start"));
    }
    public DynamicObject findObj(DynamicObject obj)
    {
        List<DynamicObject> dl = obj is Pin ? ALLPINS[(int)obj.rarity] : ALLBALLS[(int)obj.rarity];
        foreach (DynamicObject d in dl)
        {
            if (d.OBJID == obj.OBJID)
                return d;
        }
        return null;
    }
}
