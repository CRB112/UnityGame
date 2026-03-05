using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    public string OBJID;
    public GameObject prefab;
    public RarityLevel rarity;
    private int Cost;
    private Color color;

    [SerializeField, TextArea(3, 6)]
    private string description;
    public string Description => description;

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
    public Effect ApplyEffect(Effect e)
    {
        // Check if the effect type already exists
        Effect existing = GetComponent(e.GetType()) as Effect;

        if (existing != null)
        {
            existing.resetTimer();
            return existing; // return the existing effect
        }
        else
        {
            // Add new effect
            Effect newEffect = (Effect)gameObject.AddComponent(e.GetType());
            newEffect.INIT(e.duration, e.tick, this);
            newEffect.effectStart();
            return newEffect; // return the newly added effect
        }
    }
    public void ApplyEffectsToTarget(DynamicObject target)
    {
        if (this is Pin || this is Ball)
        {
            foreach (Effect e in GetComponents<Effect>())
            {
                if (e.spreadEffect())
                    target.ApplyEffect(e);
            }
            target.recalculateMulti();
        }
    }
    public virtual void recalculateMulti()
    {
        
    }
}
