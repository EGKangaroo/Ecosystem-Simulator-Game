using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaturityStage
{
    immature,
    mature,
    dead
}

public abstract class LifeForm : ScriptableObject
{
    public Color graphLineColor;

    public GameObject associatedModel;

    public string speciesName;

    [Min(0)]
    public int maximumLifespan;
    [Min(0)]
    public int minimumLifespan;
    [Min(0)]
    public int ageToMaturity;
    [Min(0)]
    public int baseReproductionTime;
    [Min(0)]
    public int baseHealth;

    public LifeForm[] likedSpecies;
    public LifeForm[] dislikedSpecies;

    public abstract void InitiateLifeForm(GameTile tile);

    private void OnValidate()
    {
        if(minimumLifespan > maximumLifespan)
        {
            minimumLifespan = maximumLifespan;
        }
    }
}
