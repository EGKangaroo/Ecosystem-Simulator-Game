using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTile : MonoBehaviour
{
    [Min(0)]
    [SerializeField]
    private float posVariation;

    private bool mousedOver = false;

    public WorldModel data;
    public GameTile tileRepresented;
    public PlayerController controller;

    public Transform plantSlot;

    public void Init(GameTile tile, PlayerController controller)
    {
        this.data = WorldModel.GetInstance();
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
                    if(controller.GetSelectedLifeform() != null)
                    {
                        data.AddNewLifeForm(tileRepresented, controller.GetSelectedLifeform());
                    }
                    else
                    {
                        Debug.Log("Can't place nothing");
                    }
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
            PlantInstance data = tileRepresented.GetPlant();
            GameObject model = data.Species.associatedModel;
            GameObject placedModel = Instantiate(model, plantSlot);
            placedModel.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);

            float randX = Random.Range(-posVariation, posVariation);
            float randZ = Random.Range(-posVariation, posVariation);

            placedModel.transform.localPosition = new Vector3(randX, placedModel.transform.localPosition.y, randZ);
        }
    }
}
