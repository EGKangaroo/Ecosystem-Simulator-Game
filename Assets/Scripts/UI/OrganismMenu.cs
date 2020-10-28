using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismMenu : MonoBehaviour
{
    public OrganismCard card;
    public Transform contentTransform;

    // Start is called before the first frame update
    void Start()
    {
        LifeForm[] lifeforms = Resources.LoadAll<LifeForm>("");
        foreach(var item in lifeforms)
        {
            OrganismCard menuItem = Instantiate(card, contentTransform);
            menuItem.SetInfo(item);
        }
    }
}
