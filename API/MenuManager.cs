using System;
using System.Collections.Generic;
using System.Linq;
using MenuLib.API.Factories;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MenuLib.API;

public class MenuManager
{
    public static MenuManager Instance { get; private set; } = null!;

    private GameObject _frameMenu = null;
    private GameObject _cacheLocation = null;
    
    internal MenuManager()
    {
        Instance = this;
        PrepareCache();
    }

    private List<Menu> GetMenus()
    {
        List<Menu> menus = [];
        
        for (int i = 0; i < _frameMenu.transform.childCount; i++)
        {
            GameObject child = _frameMenu.transform.GetChild(i).gameObject;
            // By checking if the child has a MenuLocation we can verify if it's a menu.
            if(child.GetComponent<MenuLocation>() == null) continue;
            
            menus.Add(MenuFactory.FromExistingMenu(child));
        }
        
        return menus;
    }

    public List<Menu> Menus => GetMenus();

    private void PrepareCache()
    {
        _frameMenu = GameObject.Find("MenuGame/Canvas/FrameMenu");
        _cacheLocation = new GameObject();
        _cacheLocation.transform.parent = _frameMenu.transform;
        _cacheLocation.name = "MenuLibCache";
        _cacheLocation.active = false;
        
        GameObject locationMenu = GameObject.Find("MenuGame/Canvas/FrameMenu/Location Menu");
        GameObject cachedLocationMenu = Object.Instantiate(locationMenu, _cacheLocation.transform);
        cachedLocationMenu.name = "Menu";
        
        for (int i = 0; i < cachedLocationMenu.transform.childCount; i++)
        {
            GameObject child = cachedLocationMenu.transform.GetChild(i).gameObject;
            if (child.name == "Text")
            {
                GameObject grandChild = child.gameObject;
                Object.Destroy(grandChild.GetComponent<Localization_UIText>());
            }
            for (int j = 0; j < child.transform.childCount; j++)
            {
                GameObject grandChild = child.transform.GetChild(j).gameObject;
                Object.Destroy(grandChild.GetComponent<Localization_UIText>());
                // TODO: Fix fonts
            }
        }
    }

    public Menu Find(string name)
    {
        foreach (Menu menu in Menus)
        {
            if(menu.MenuObject.gameObject.name.ToLower().Equals(name.ToLower())) return menu;
        }

        return null;
    }

    internal GameObject CreateMenuFromTemplate()
    {
        GameObject menu = Object.Instantiate(_cacheLocation.transform.Find("Menu").gameObject, _frameMenu.transform);
        List<GameObject> childrenThatShouldBeOrphaned = new List<GameObject>();
        GameObject textObject = null;
        for (int i = 0; i < menu.transform.childCount; i++)
        {
            GameObject child = menu.transform.GetChild(i).gameObject;
            if (child.name != "Text")
            {
                childrenThatShouldBeOrphaned.Add(child);
            }
            else
            {
                textObject = child;
            }
        }

        foreach (GameObject child in childrenThatShouldBeOrphaned)
        {
            // Since game objects are only destroyed at the end of the frame we detach the children
            // which makes sure that future menu option creation won't be impacted
            child.transform.SetParent(null);
                
            Object.Destroy(child);
        }
        
        MenuLocation menuLocation = menu.GetComponent<MenuLocation>();
        menuLocation.countCases = 0;
        menuLocation.objects.Clear();
        menuLocation.buttonBack = null;

        if (textObject != null)
        {
            menuLocation.objects.Add(textObject.GetComponent<RectTransform>());
        }

        menu.active = false;
        
        return menu;
    }
    
    internal GameObject CreateOptionFromTemplate(Menu parent)
    {
        return Object.Instantiate(_cacheLocation.transform.Find("Menu/Button NewGame").gameObject, parent.MenuObject.transform);
    }
}