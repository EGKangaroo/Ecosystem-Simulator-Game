using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganismCard : MonoBehaviour
{
    private PlayerController controller;
    private LifeForm lifeformRepresented;

    public Button selectButton;
    public Text label;

    // Start is called before the first frame update
    void Start()
    {
        controller = PlayerController.GetInstance();
    }

    void ChangeLifeForm()
    {
        controller.selectedLifeform = lifeformRepresented;
    }

    public void SetInfo(LifeForm life)
    {
        this.lifeformRepresented = life;
        Debug.Log(this.lifeformRepresented.speciesName);
        selectButton.onClick.AddListener(ChangeLifeForm);
        label.text = lifeformRepresented.speciesName;
    }
}
