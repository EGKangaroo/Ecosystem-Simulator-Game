using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SimSystem : MonoBehaviour
{
    private int stepsSimulated = 0;

    public WorldData data;

    [Range(0f, 1f)]
    public float simulationStepDuration;
    private float secondsElapsed;

    public bool paused = true;

    public UnityEvent stepDone = new UnityEvent();

    private void Start()
    {
        secondsElapsed = 0f;
    }

    public float GetSpeed()
    {
        return simulationStepDuration;
    }

    public void IncreaseSpeed()
    {
        simulationStepDuration /= 2;
        if(simulationStepDuration < 0.125f)
        {
            simulationStepDuration = 0.125f;
        }
    }

    public void DecreaseSpeed()
    {
        simulationStepDuration *= 2;
        if(simulationStepDuration >= 32f)
        {
            simulationStepDuration = 32f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            secondsElapsed += Time.deltaTime;
        }

        if(secondsElapsed > simulationStepDuration)
        {
            SimulateStep();
            secondsElapsed = 0;
        }
    }

    void SimulateStep()
    {
        //get all the lifeforms
        List<LifeFormData> life = data.GetAllLifeForms();

        //age up all lifeforms
        foreach(var item in life)
        {
            item.Advance();
        }

        foreach(var tile in data.ReadMap())
        {
            if(tile.GetPlant() != null)
            {
                PlantData plant = tile.GetPlant();

                //Handle reproduction
                HandleReproduction(plant, tile);

                //handle environmental damage
                HandleEnvironmentDamage(plant, tile);

                //Handle death
                HandleDeath(plant, tile);
            }
        }

        stepDone.Invoke();
        stepsSimulated++;
    }

    public void AutoSimulateNumberOfSteps(int steps)
    {
        for(int i = 0; i < steps; i++)
        {
            SimulateStep();
        }
    }

    public int GetNumberOfStepsSimulated()
    {
        return stepsSimulated;
    }

    private void HandleEnvironmentDamage(PlantData plant, GameTile tile)
    {
        List<Coords> coordinates = tile.neighbouringCoords;
        int damageCounter = 0;
        bool aroundLikedSpecies = false;
        foreach (var item in coordinates)
        {
            GameTile neighbour = data.GetTileByCoord(item);
            PlantData neighbourPlant = neighbour.GetPlant();
            if (neighbourPlant != null)
            {
                if (plant.Species.dislikedSpecies.Contains(neighbourPlant.Species))
                {
                    damageCounter++;
                }
                if (!aroundLikedSpecies)
                {
                    if (plant.Species.likedSpecies.Contains(neighbourPlant.Species))
                    {
                        aroundLikedSpecies = true;
                    }
                }
            }
        }
        if (!aroundLikedSpecies)
        {
            damageCounter++;
        }
        plant.Damage(damageCounter);
    }

    private void HandleDeath(PlantData plant, GameTile tile)
    {
        if (plant.Dead())
        {
            //handle deaths
            tile.DeletePlant();
        }
    }

    private void HandleReproduction(PlantData plant, GameTile tile)
    {
        if (plant.ReadyToReproduce())
        {
            //handle lifeform reproduction
            List<Coords> neighbours = tile.neighbouringCoords;
            int numberOfNeighbours = neighbours.Count;
            int r = Random.Range(0, numberOfNeighbours);

            Coords chosenCoord = neighbours[r];
            GameTile chosenTile = data.GetTileByCoord(chosenCoord);

            if (chosenTile.occupyingPlant == null)
            {
                chosenTile.SetPlant(new PlantData((Plant)tile.GetPlant().Species));
            }

            tile.GetPlant().ResetReproduction();
        }
    }
}
