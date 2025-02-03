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
        return null;
    }
    
    internal GameObject CreateOptionFromTemplate(Menu parent)
    {
        return Object.Instantiate(_cacheLocation.transform.Find("Menu/Button NewGame").gameObject, parent.MenuObject.transform);
    }
}