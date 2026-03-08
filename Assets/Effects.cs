using System.Collections;
using UnityEngine;

public class Fire : Effect
{

    protected override void Start()
    {
        spreadChance = 50;
        ballModifier = 1.2f;
        pinModifier = 1.2f;
        op = Operation.Multiply;
        base.Start();
    }
}