using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTile : MonoBehaviour
{
    public WorldData data;
    public GameTile tileRepresented;
    public PlayerController controller;

    public Transform plantSlot;

    public void Init(WorldData data, GameTile tile, PlayerController controller)
    {
        this.data = data;
        this.tileRepresented = tile;
        tileRepresented.PlantChanged.AddListener(UpdatePlantModel);
        this.controller = controller;
    }

    private void Start()
    {
        controller = PlayerController.GetInstance();
        tileRepresented.PlantChanged.AddListener(UpdatePlantModel);
    }

    //handle what happens when you click on this tile
    public void OnMouseDown()
    {
        if(controller != null)
        {
            data.AddNewLifeForm(tileRepresented, controller.GetSelectedLifeform());
            Debug.Log("New Lifeform added");
        }
    }

    public void UpdatePlantModel()
    {
        Debug.Log("Test");
        foreach(Transform child in plantSlot)
        {
            Destroy(child.gameObject);
        }
        if(tileRepresented.GetPlant() != null)
        {
            PlantData data = tileRepresented.GetPlant();
            GameObject model = data.species.associatedModel;
            Instantiate(model, plantSlot);
        }
    }
}
