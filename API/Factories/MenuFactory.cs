using System;
using UnityEngine;

namespace MenuLib.API.Factories;

public class MenuFactory
{
    private string _objectName = null;
    private Menu _backMenu = null;
    private string _title = null;
    
    internal static Menu FromExistingMenu(GameObject existingMenu)
    {
        return new Menu(existingMenu);
    }

    public MenuFactory SetObjectName(string objectName)
    {
        _objectName = objectName;
        return this;
    }

    public MenuFactory SetBackButton(Menu gotoMenu)
    {
        _backMenu = gotoMenu;
        return this;
    }
    
    public MenuFactory SetTitle(string title)
    {
        _title = title;
        return this;
    }

    public Menu Build()
    {
        if(_title == null) throw new Exception("Title must be set before building a menu");
        GameObject menuGo = MenuManager.Instance.CreateMenuFromTemplate();
        if(_objectName != null) menuGo.name = _objectName;
        
        MenuLocation menuLocation = menuGo.GetComponent<MenuLocation>();
        
        Menu menu =  FromExistingMenu(menuGo);
        
        menu.Title = _title;
        menu.TextComponent.font = GlobalGame.fontUse;

        if (_backMenu != null)
        {
            MenuOption backButton = new MenuOptionFactory()
                .SetName("BACK")
                .SetParent(menu)
                .SetNextLocation(_backMenu)
                .Build();
        
            menuLocation.buttonBack = backButton.OptionObject;
        }

        return menu;
    }
}