using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    public string OBJID;
    public GameObject prefab;
    public RarityLevel rarity;
    private int Cost;
    private Color color;
    public void Init()
    {
        color = Rarity.GetColor(rarity);
        Cost = Rarity.GetCost(rarity);
        prefab = gameObject;
        OBJID = gameObject.name;
    }


    void Update()
    {

    }
    public Color getColor()
    {
        return color;
    }
    public int getCost()
    {
        return Cost;
    }
}
