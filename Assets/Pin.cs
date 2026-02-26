using UnityEngine;

public class Pin : DynamicObject
{
    public Collider2D myCollider;
    void Start()
    {
        rarity = RarityLevel.Common;
    }

    void Update()
    {

    }
}
