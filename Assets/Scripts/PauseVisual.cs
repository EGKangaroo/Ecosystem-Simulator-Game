using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseVisual : MonoBehaviour
{
    Toggle tog;
    public Text text;
    public SimSystem system;

    public string onText;
    public string offText;

    public void SetButtonText()
    {
        if (tog.isOn)
        {
            text.text = onText;
        }
        if (!tog.isOn)
        {
            text.text = offText;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tog = GetComponent<Toggle>();
        tog.SetIsOnWithoutNotify(system.paused);
        SetButtonText();
    }
}
