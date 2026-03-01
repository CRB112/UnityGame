using System.Collections.Generic;
using UnityEngine;

public class Pin : DynamicObject
{
    public BoxCollider2D myCollider;
    public List<Tile> myTiles = new List<Tile>();
    void Start()
    {
        rarity = RarityLevel.Common;
    }

    void Update()
    {

    }
    public void colorMyTiles(Color c)
    {
        foreach (Tile t in myTiles)
        {
            t.GetComponent<SpriteRenderer>().color = c;
        }
    }
    public void OnDestroy()
    {
        foreach (Tile t in myTiles)
        {
            t.taken = false;
            t.setColor(Color.clear);
        }
    }
}
