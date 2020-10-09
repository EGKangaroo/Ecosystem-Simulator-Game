using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaturityStage
{
    immature,
    mature
}

public abstract class LifeForm : ScriptableObject
{
    public string speciesName;

    public int maximumLifespan;
    public int ageToMaturity;
    public int baseReproductionTime;
    public int baseHealth;

    public LifeForm[] likedSpecies;
    public LifeForm[] dislikedSpecies;
}
