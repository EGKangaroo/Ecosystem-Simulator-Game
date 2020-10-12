using System.Collections;
using System.Collections.Generic;
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
                //Handle reproduction
                if (tile.GetPlant().ReadyToReproduce())
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

                //handle environmental damage

                //Handle death
                if (tile.GetPlant().Dead())
                {
                    //handle deaths
                    tile.DeletePlant();
                }
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
}
