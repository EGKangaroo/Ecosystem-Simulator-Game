using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeForm : ScriptableObject
{
    public string species;
    public int baseHealth;
    public int baseReproductionTime;
    public int maxAge;
    public int ageToMaturity;

    public LifeForm[] likedSpecies;
    public LifeForm[] dislikedSpecies;
}
