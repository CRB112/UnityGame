using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Base Ball Class
public class Ball : DynamicObject
{
    private StatManager statManager;
    public float scoreMultiBase;
    public float scoreMultiModified;

    private float maxSpeed = 30f;
    private Rigidbody2D rb;
    void Start()
    {
        gameObject.AddComponent<Fire>();
        rarity = RarityLevel.Common;
        statManager = FindFirstObjectByType<StatManager>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        DynamicObject obj = collision.gameObject.GetComponent<DynamicObject>();
        if (obj == null)
            return;
        if (obj is Scoreable s)
        {
            s.activateObj(this);
        }
        if (obj is Scoreable || obj is Ball)
        {
            ApplyEffectsToTarget(obj);
            obj.ApplyEffectsToTarget(this);
        }


    }
    public override void recalculateMulti()
    {
        foreach (Effect e in GetComponents<Effect>())
        {
            scoreMultiModified = scoreMultiBase;
            scoreMultiModified = e.getBallModifier(scoreMultiModified);
        }
    }
}
