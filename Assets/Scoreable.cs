using System.Collections.Generic;
using UnityEngine;

public class Scoreable : DynamicObject
{
    public StatManager statMan;

    public BoxCollider2D myCollider;
    public List<Tile> myTiles = new List<Tile>();

    public float score;

    protected virtual void Start()
    {
        Debug.Log("STARTED");
        statMan = FindAnyObjectByType<StatManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float getScore() { return score; }

    public void activateObj(Ball b = null)
    {
        if (b != null)
        {
            statMan.addScore((int)(score * b.scoreMulti));
        }
        else
        {
            statMan.addScore((int)score);
        }
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
