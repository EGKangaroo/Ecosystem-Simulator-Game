using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimSystem : MonoBehaviour
{
    private int stepsSimulated = 0;

    public WorldData data;

    [Range(0, 30)]
    public int simulationStepsPerSecond;

    private float simulationStepDuration;
    private float secondsElapsed;

    public bool paused = true;

    public UnityEvent stepDone = new UnityEvent();

    private void Start()
    {
        simulationStepDuration = 1f / simulationStepsPerSecond;
        secondsElapsed = 0f;
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

        //handle lifeform reproduction

        //handle environmental damage

        //handle deaths
        foreach(var tile in data.ReadMap())
        {
            if(tile.GetPlant() != null)
            {
                if (tile.GetPlant().Dead())
                {
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
