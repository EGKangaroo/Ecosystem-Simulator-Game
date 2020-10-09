using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LifeFormData
{
    public LifeForm species;

    public int currentAge;
    public MaturityStage currentMaturity;

    public int currentHealth;

    public int CurrentReproductionTime()
    {
        int baseLevel = species.baseReproductionTime;
        int modifier = species.baseReproductionTime * (1 - currentHealth / species.baseHealth);
        return baseLevel + modifier;
    }

    public int timeSinceLastReproduction;

    public LifeFormData(LifeForm species)
    {
        this.species = species;
        currentAge = 0;
        currentMaturity = currentAge > species.ageToMaturity ? MaturityStage.mature : MaturityStage.immature;
        currentHealth = species.baseHealth;
        timeSinceLastReproduction = 0;
    }
}
