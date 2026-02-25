using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    public RarityLevel rarity;
    private int Cost;
    private Color color;
    public void Init()
    {
        color = Rarity.GetColor(rarity);
        Cost = Rarity.GetCost(rarity);
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
