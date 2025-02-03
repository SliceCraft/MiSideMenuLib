﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MenuLib.API;

public class MenuOption
{
    public GameObject OptionObject { get; private set; }

    internal MenuOption(GameObject originalOption)
    {
        OptionObject = originalOption;
    }
    
    public string Text {
        get => OptionObject.GetComponentInChildren<Text>().text;
        set => OptionObject.GetComponentInChildren<Text>().text = value;
    }

    public GameObject NextLocation
    {
        get => OptionObject.GetComponent<MenuNextLocation>().nextLocation;
        set => OptionObject.GetComponent<MenuNextLocation>().nextLocation = value;
    }

    public UnityEvent OnClick
    {
        get => OptionObject.GetComponent<ButtonMouseClick>().eventClick;
        set => OptionObject.GetComponent<ButtonMouseClick>().eventClick = value;
    }
}