using System;
using System.Collections;
using UnityEngine;
public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo
}

public class Effect : MonoBehaviour
{
    public float duration = 5;
    public float tick = .5f;
    public float spreadChance;
    public DynamicObject d;

    public float ballModifier;
    public float pinModifier;
    public Operation op;

    private Coroutine effectRoutine;

    public void INIT(float dur, float t, DynamicObject dO)
    {
        duration = dur;
        tick = t;
        d = dO;
    }
    protected virtual void Start()
    {
        //Assumption
        d = GetComponent<DynamicObject>();
        effectStart();
    }
    void Update()
    {

    }
    public void effectStart()
    {
        effectRoutine = StartCoroutine(effectStartEnum(duration));
    }
    IEnumerator effectStartEnum(float dur)
    {
        while (dur >= 0)
        {
            yield return new WaitForSeconds(tick);
            dur -= tick;
            if (d is Ball)
                ApplyBallEffect();
            else if (d is Pin)
                ApplyPinEffect();
            //Do your effect
        }
        Destroy(this);
    }
    public virtual void ApplyBallEffect()
    {
    }
    public virtual void ApplyPinEffect()
    {

    }
    public void resetTimer()
    {
        if (effectRoutine != null) StopCoroutine(effectRoutine);
        effectRoutine = StartCoroutine(effectStartEnum(duration));
    }
    public bool spreadEffect() {
        return UnityEngine.Random.Range(0, 101) <= spreadChance;
    }
    public virtual float getBallModifier(float baseVal)
    {
        return ApplyOperation(baseVal, ballModifier, op);
    }
    public virtual float getPinModifier(float baseVal)
    {
        return ApplyOperation(baseVal, pinModifier, op);
    }
    protected float ApplyOperation(float baseVal, float mod, Operation o)
    {
        return op switch
        {
            Operation.Add => baseVal + mod,
            Operation.Multiply => baseVal * mod,
            Operation.Divide => baseVal / mod,
            Operation.Modulo => baseVal % mod,
            Operation.Subtract => baseVal - mod,
            _ => baseVal,
        };
    }
}
