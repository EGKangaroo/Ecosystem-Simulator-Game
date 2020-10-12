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
    }

    //handle what happens when you click on this tile
    public void OnMouseDown()
    {
        if(controller != null)
        {
            bool mouseOnUIObject = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (!mouseOnUIObject)
            {
                //do action
                data.AddNewLifeForm(tileRepresented, controller.GetSelectedLifeform());
            }
        }
    }

    public void UpdatePlantModel()
    {
        foreach(Transform child in plantSlot)
        {
            Destroy(child.gameObject);
        }
        if(tileRepresented.GetPlant() != null)
        {
            PlantData data = tileRepresented.GetPlant();
            GameObject model = data.Species.associatedModel;
            Instantiate(model, plantSlot);
        }
    }
}
