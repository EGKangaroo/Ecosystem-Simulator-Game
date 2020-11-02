using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataCollector : MonoBehaviour
{
    public UnityEvent DataUpdated = new UnityEvent();

    public int maxHistory = 20;
    public WorldModel data;

    List<int> plantsPerStep = new List<int>();

    Dictionary<LifeForm, List<int>> populationBySpecies = new Dictionary<LifeForm, List<int>>();

    private void Start()
    {
        LifeForm[] allLifeForms = Resources.LoadAll<LifeForm>("");
        foreach(var item in allLifeForms)
        {
            populationBySpecies.Add(item, new List<int>());
        }
    }

    public void CollectNewDataPoint()
    {
        int i = 0;
        List<LifeFormInstance> lifeForms = data.GetAllLifeForms();
        foreach(var item in populationBySpecies)
        {
            item.Value.Add(0);
        }
        foreach(var item in lifeForms)
        {
            i = populationBySpecies[item.Species].Count();
            populationBySpecies[item.Species][i - 1]++;
        }
        if(i > maxHistory)
        {
            foreach(var item in populationBySpecies)
            {
                item.Value.RemoveAt(0);
            }
        }
        DataUpdated.Invoke();
    }

    public List<int> GetData()
    {
        return plantsPerStep;
    }

    public Dictionary<LifeForm, List<int>> GetPopulationData()
    {
        return populationBySpecies;
    }
}
