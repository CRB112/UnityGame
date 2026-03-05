using System.Collections.Generic;
using UnityEngine;

public class Scoreable : Buildable
{
    public StatManager statMan;


    public float scoreBase;
    public float scoreModified;

    protected virtual void Start()
    {
        statMan = FindAnyObjectByType<StatManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float getScore() { return scoreModified; }

    public override void activateObj(Ball b = null)
    {
        if (b != null)
        {
            statMan.addScore((int)(scoreModified * b.scoreMultiModified));
        }
        else
        {
            statMan.addScore((int)scoreModified);
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
    public override void recalculateMulti()
    {
        foreach (Effect e in GetComponents<Effect>())
        {
            scoreModified = scoreBase;
            scoreModified = e.getBallModifier(scoreModified);
        }
    }
}
