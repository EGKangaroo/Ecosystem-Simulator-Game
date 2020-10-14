using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTile : MonoBehaviour
{
    private bool mousedOver = false;

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

    private void Update()
    {
        if (controller != null)
        {
            bool mouseOnUIObject = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (!mouseOnUIObject && mousedOver)
            {
                //do action
                if (Input.GetMouseButtonDown(0))
                {
                    data.AddNewLifeForm(tileRepresented, controller.GetSelectedLifeform());
                }
                if (Input.GetMouseButtonDown(1))
                {
                    data.AddNewLifeForm(tileRepresented, controller.GetSecondSelectedLifeform());
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        mousedOver = true;
    }

    private void OnMouseExit()
    {
        mousedOver = false;
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
