using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataCollector : MonoBehaviour
{
    public UnityEvent DataUpdated = new UnityEvent();

    public int maxHistory = 1000;
    public WorldData data;

    List<int> plantsPerStep = new List<int>();

    public void CollectNewDataPoint()
    {
        List<LifeFormData> lifeForms = data.GetAllLifeForms();
        plantsPerStep.Add(lifeForms.Count);
        if(plantsPerStep.Count > maxHistory)
        {
            plantsPerStep.RemoveAt(0);
        }
        DataUpdated.Invoke();
    }

    public List<int> GetData()
    {
        return plantsPerStep;
    }
}
