using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MenuLib.API.Factories;

public class MenuOptionFactory
{
    private string _name;
    private GameObject _nextLocation;
    private UnityAction _onClickAction;
    private GameMenu _parent;
    private int _placeBefore = -1;
    private string _objectName;
    
    public MenuOptionFactory SetName(string name)
    {
        _name = name;
        return this;
    }

    public MenuOptionFactory SetNextLocation(GameMenu nextLocation)
    {
        _nextLocation = nextLocation.MenuObject;
        return this;
    }

    public MenuOptionFactory SetOnClick(Action unityAction)
    {
        _onClickAction = unityAction;
        return this;
    }

    public MenuOptionFactory SetParent(GameMenu parent)
    {
        _parent = parent;
        return this;
    }

    public MenuOptionFactory PlaceOptionBefore(int placeBefore)
    {
        _placeBefore = placeBefore;
        return this;
    }

    public MenuOptionFactory SetObjectName(string name)
    {
        _objectName = name;
        return this;
    }

    public MenuOption Build()
    {
        if(_name == null || _parent == null || _nextLocation == null) throw new Exception("Name, Parent and NextLocation must be set before building an option");
        List<MenuOption> menuOptions = _parent.MenuOptions;
        float yPos;
        if (_placeBefore == -1)
        {
            if (menuOptions.Count > 0)
            {
                yPos = menuOptions[^1].OptionObject.transform.localPosition.y - 55;
            }
            else
            {
                yPos = -100;
            }
        }
        else
        {
            yPos = menuOptions[_placeBefore].OptionObject.transform.localPosition.y;
        }
            
        GameObject optionObject = MenuManager.Instance.CreateOptionFromTemplate(_parent);
        
        if(_objectName != null) optionObject.name = _objectName;
        
        MenuOption menuOption = new MenuOption(optionObject)
        {
            Text = _name,
            TextComponent =
            {
                font = GlobalGame.fontUse
            },
            NextLocation = _nextLocation
        };
        menuOption.OnClick.m_PersistentCalls.m_Calls.Clear();
        if(_onClickAction != null) menuOption.OnClick.AddListener(_onClickAction);
        
        Localization_UIText uiText = menuOption.TextComponent.GetComponent<Localization_UIText>();
        if (uiText != null)
        {
            UnityEngine.Object.Destroy(uiText);
        }
        
        Vector3 localPos = menuOption.OptionObject.transform.localPosition;
        localPos.y = yPos;
        menuOption.OptionObject.transform.localPosition = localPos;

        if (_placeBefore >= 0)
        {
            for (int i = _placeBefore; i < menuOptions.Count; i++)
            {
                Vector3 localPosition = menuOptions[i].OptionObject.transform.localPosition;
                localPosition.y -= 55;
                menuOptions[i].OptionObject.transform.localPosition = localPosition;
            }
        }

        MenuLocation menuLocation = _parent.MenuObject.GetComponent<MenuLocation>();
        menuLocation.countCases += 1;
        RectTransform rectTransform = optionObject.GetComponent<RectTransform>();
        menuLocation.objects.Add(rectTransform);
        Sort(menuLocation.objects);
        
        return menuOption;
    }
    
    public MenuOption BuildMenuDivider()
    {
        if(_parent == null) throw new Exception("Parent must be set before building a menu divider");
        List<MenuOption> menuOptions = _parent.MenuOptions;
        float yPos;
        if (_placeBefore == -1)
        {
            if (menuOptions.Count > 0)
            {
                // Why is this value 1 different from the usual?
                // Talk to MakenCat, he decided that
                yPos = menuOptions[^1].OptionObject.transform.localPosition.y - 56;
            }
            else
            {
                yPos = -100;
            }
        }
        else
        {
            // Why minus one?
            // Same reason why everything else in this dumbfuckery is working the way it is
            yPos = menuOptions[_placeBefore].OptionObject.transform.localPosition.y - 1;
        }
            
        GameObject optionObject = MenuManager.Instance.CreateDividerFromTemplate(_parent);
        
        if(_objectName != null) optionObject.name = _objectName;
        
        MenuOption menuOption = new MenuOption(optionObject);
        
        Vector3 localPos = menuOption.OptionObject.transform.localPosition;
        localPos.y = yPos;
        menuOption.OptionObject.transform.localPosition = localPos;

        if (_placeBefore >= 0)
        {
            for (int i = _placeBefore; i < menuOptions.Count; i++)
            {
                Vector3 localPosition = menuOptions[i].OptionObject.transform.localPosition;
                // You know what would be a funny idea?
                // It'd be so funny if MakenCat decided that these should be 9 instead of 10
                localPosition.y -= 9;
                menuOptions[i].OptionObject.transform.localPosition = localPosition;
            }
        }

        MenuLocation menuLocation = _parent.MenuObject.GetComponent<MenuLocation>();
        menuLocation.countCases += 1;
        RectTransform rectTransform = optionObject.GetComponent<RectTransform>();
        menuLocation.objects.Add(rectTransform);
        Sort(menuLocation.objects);
        
        return menuOption;
    }

    private static void Sort(Il2CppSystem.Collections.Generic.List<RectTransform> list)
    {
        List<RectTransform> list2 = [];
        for (int i = 0; i < list.Count; i++)
        {
            list2.Add((RectTransform) list[(Index)i]);
        }

        list2 = list2.OrderBy(e => -1 * e.localPosition.y).ToList();
        
        for (int i = 0; i < list2.Count; i++)
        {
            list[(Index)i] = list2[i];
        }
    }
}