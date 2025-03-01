using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MenuLib.API.Factories;

public class MenuFactory
{
    private string _objectName = null;
    private GameMenu _backGameMenu = null;
    private string _title = null;
    
    internal static GameMenu FromExistingMenu(GameObject existingMenu)
    {
        return new GameMenu(existingMenu);
    }

    public MenuFactory SetObjectName(string objectName)
    {
        _objectName = objectName;
        return this;
    }

    public MenuFactory SetBackButton(GameMenu gotoGameMenu)
    {
        _backGameMenu = gotoGameMenu;
        return this;
    }
    
    public MenuFactory SetTitle(string title)
    {
        _title = title;
        return this;
    }

    public GameMenu Build()
    {
        if(_title == null) throw new Exception("Title must be set before building a menu");
        GameObject menuGo = MenuManager.Instance.CreateMenuFromTemplate();
        if(_objectName != null) menuGo.name = _objectName;
        
        MenuLocation menuLocation = menuGo.GetComponent<MenuLocation>();
        
        GameMenu gameMenu =  FromExistingMenu(menuGo);
        
        gameMenu.Title = _title;
        gameMenu.TextComponent.font = GlobalGame.fontUse;
        Localization_UIText uiText = gameMenu.TextComponent.GetComponent<Localization_UIText>();
        if (uiText != null)
        {
            Object.Destroy(uiText);
        }

        if (_backGameMenu != null)
        {
            MenuOption backButton = new MenuOptionFactory()
                .SetName("BACK")
                .SetParent(gameMenu)
                .SetNextLocation(_backGameMenu)
                .Build();
        
            menuLocation.buttonBack = backButton.OptionObject;
        }

        return gameMenu;
    }
}