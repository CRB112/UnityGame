using System.Collections;
using UnityEngine;

public class Fire : Effect
{
    protected override void Start()
    {
        spreadChance = 50;
        base.Start();
    }
    public override void ApplyBallEffect()
    {
    }
        public override void ApplyPinEffect()
    {
    }
}