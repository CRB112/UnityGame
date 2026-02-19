using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    public RarityLevel rarity;
    private int Cost;
    private Color color;
    void Start()
    {
        color = Rarity.GetColor(rarity);
        Cost = Rarity.GetCost(rarity);
    }


    void Update()
    {

    }
}
