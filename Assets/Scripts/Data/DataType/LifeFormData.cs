using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LifeFormData
{
    public LifeForm Species { get; set; }

    public int currentAge;
    public int timeSinceLastReproduction;
    public int currentHealth;

    public MaturityStage CurrentMaturity()
    {
        return currentAge >= Species.ageToMaturity ? MaturityStage.mature : MaturityStage.immature;
    }

    public int CurrentReproductionTime()
    {
        int baseLevel = Species.baseReproductionTime;
        int modifier = Species.baseReproductionTime * (1 - currentHealth / Species.baseHealth);
        return baseLevel + modifier;
    }

    public bool ReadyToReproduce()
    {
        return CurrentReproductionTime() <= timeSinceLastReproduction;
    }

    public LifeFormData(LifeForm species)
    {
        this.Species = species;
        currentAge = 0;
        currentHealth = species.baseHealth;
        timeSinceLastReproduction = 0;
    }

    public void Advance()
    {
        currentAge++;
        timeSinceLastReproduction++;
    }

    public bool Dead()
    {
        return currentAge > Species.maximumLifespan || currentHealth <= 0;
    }

    public void Damage(int healthDamage)
    {
        currentHealth -= healthDamage;
    }

    public void ResetReproduction()
    {

    }
}
