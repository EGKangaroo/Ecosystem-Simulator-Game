using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaturityStage
{
    Young,
    Adult,
    Dead
}

[System.Serializable]
public class PlantInstance : LifeFormInstance
{
    public Plant plantType;

    private int currentHealth;
    private int currentAge = 0;
    private MaturityStage currentMaturity;

    private int modifiedReproductionTime;
    private int timeSinceReproduction = 0;

    public PlantInstance(Plant plant)
    {
        plantType = plant;
        Initialise();
    }

    void Initialise()
    {
        currentHealth = plantType.baseHealth;
        currentMaturity = currentAge < plantType.ageToMaturity ? MaturityStage.Young : MaturityStage.Adult;
        modifiedReproductionTime = plantType.baseReproductionTime * 2 - plantType.baseReproductionTime * (plantType.baseHealth / currentHealth);
    }
}
