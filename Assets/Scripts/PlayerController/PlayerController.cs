using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Plant selectedLifeform;
    public Plant secondSelectedLifeform;

    private static PlayerController _instance;

    public static PlayerController GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Plant GetSelectedLifeform()
    {
        return selectedLifeform;
    }

    public Plant GetSecondSelectedLifeform()
    {
        return secondSelectedLifeform;
    }
}
