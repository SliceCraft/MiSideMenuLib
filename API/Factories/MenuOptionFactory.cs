using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MenuLib.API.Factories;

public class MenuOptionFactory
{
    private string Name = null;
    private GameObject NextLocation = null;
    private UnityAction OnClickAction = null;
    private Menu Parent = null;
    private int PlaceBefore = -1;
    
    public MenuOptionFactory SetName(string name)
    {
        Name = name;
        return this;
    }

    public MenuOptionFactory SetNextLocation(Menu nextLocation)
    {
        NextLocation = nextLocation.MenuObject;
        return this;
    }

    public MenuOptionFactory SetOnClick(Action unityAction)
    {
        OnClickAction = unityAction;
        return this;
    }

    public MenuOptionFactory SetParent(Menu parent)
    {
        Parent = parent;
        return this;
    }

    public MenuOptionFactory PlaceOptionBefore(int placeBefore)
    {
        PlaceBefore = placeBefore;
        return this;
    }
    
    public MenuOptionFactory PlaceOptionBefore(MenuOption placeBefore)
    {
        // TODO: Implement this
        return null;
    }

    public MenuOption Build()
    {
        List<MenuOption> menuOptions = Parent.MenuOptions;
        float yPos;
        if (PlaceBefore == -1)
        {
            if (menuOptions.Count > 0)
            {
                yPos = menuOptions[^1].OptionObject.transform.localPosition.y + 55;
            }
            else
            {
                yPos = -100;
            }
        }
        else
        {
            yPos = menuOptions[PlaceBefore].OptionObject.transform.localPosition.y;
        }
            
        GameObject optionObject = MenuManager.Instance.CreateOptionFromTemplate(Parent);
        MenuOption menuOption = new MenuOption(optionObject);
        menuOption.Text = Name;
        menuOption.TextComponent.font = GlobalGame.fontUse;
        menuOption.NextLocation = NextLocation;
        menuOption.OnClick.m_PersistentCalls.m_Calls.Clear();
        if(OnClickAction != null) menuOption.OnClick.AddListener(OnClickAction);
        
        Vector3 localPos = menuOption.OptionObject.transform.localPosition;
        localPos.y = yPos;
        menuOption.OptionObject.transform.localPosition = localPos;

        if (PlaceBefore >= 0)
        {
            for (int i = PlaceBefore; i < menuOptions.Count; i++)
            {
                Vector3 localPosition = menuOptions[i].OptionObject.transform.localPosition;
                localPosition.y -= 55;
                menuOptions[i].OptionObject.transform.localPosition = localPosition;
            }
        }

        MenuLocation menuLocation = Parent.MenuObject.GetComponent<MenuLocation>();
        menuLocation.countCases += 1;
        RectTransform rectTransform = optionObject.GetComponent<RectTransform>();
        menuLocation.objects.Add(rectTransform);
        Sort(menuLocation.objects);
        
        return menuOption;
    }

    private void Sort(Il2CppSystem.Collections.Generic.List<RectTransform> list)
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