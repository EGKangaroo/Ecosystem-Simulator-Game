using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LifeFormData
{
    public LifeForm species;

    public int currentAge;
    public int timeSinceLastReproduction;
    public int currentHealth;

    public MaturityStage CurrentMaturity()
    {
        return currentAge >= species.ageToMaturity ? MaturityStage.mature : MaturityStage.immature;
    }

    public int CurrentReproductionTime()
    {
        int baseLevel = species.baseReproductionTime;
        int modifier = species.baseReproductionTime * (1 - currentHealth / species.baseHealth);
        return baseLevel + modifier;
    }

    public bool ReadyToReproduce()
    {
        return CurrentReproductionTime() >= timeSinceLastReproduction;
    }

    public LifeFormData(LifeForm species)
    {
        this.species = species;
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
        return currentAge > species.maximumLifespan || currentHealth <= 0;
    }

    public void Damage(int healthDamage)
    {
        currentHealth -= healthDamage;
    }
}
