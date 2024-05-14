using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject underConstruction;

    public void OnSettingsButtonClicked()
    {
       
        underConstruction.SetActive(!underConstruction.active);
    }
}
