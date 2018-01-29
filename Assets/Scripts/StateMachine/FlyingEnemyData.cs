using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyData : EnemyData
{
    public float frequency;
    public float magnitude;
    public float maxFlightDistance;

    [HideInInspector] public float randomSineOffset;
    [HideInInspector] public float currentFlightDistance = 0;

    protected override void Start()
    {
        base.Start();

        RandomizeOffset();
    }

    private void RandomizeOffset()
    {
        randomSineOffset = Random.Range(0, 5);
    }
}
