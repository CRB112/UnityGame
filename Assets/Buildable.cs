using UnityEngine;
using System.Collections.Generic;


public class Buildable : DynamicObject
{
    public BoxCollider2D myCollider;
    public List<Tile> myTiles = new List<Tile>();

    void Start()
    {

    }

    void Update()
    {

    }
    public virtual void activateObj(Ball b = null)
    {
        //Do nothign lmfao, just a base case
    }
}
