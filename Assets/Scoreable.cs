using System.Collections.Generic;
using UnityEngine;

public class Scoreable : Buildable
{
    public StatManager statMan;


    public float score;

    protected virtual void Start()
    {
        statMan = FindAnyObjectByType<StatManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float getScore() { return score; }

    public override void activateObj(Ball b = null)
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
