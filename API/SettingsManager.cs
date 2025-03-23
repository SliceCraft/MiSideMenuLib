using MenuLib.API.Events;
using MenuLib.API.Factories;

namespace MenuLib.API;

public static class SettingsManager
{
    private static GameMenu _modsGameMenu;
    
    internal static void Init()
    {
        GameMenu gameMenu = MenuManager.Instance.Find("Location Menu");
        if (gameMenu == null)
        {
            Plugin.Log.LogError("Couldn't find the main menu");
            return;            
        }
        
        GameMenu settingsGameMenu = MenuManager.Instance.Find("Location MainOptions");
        if (settingsGameMenu == null)
        {
            Plugin.Log.LogError("Couldn't find the settings menu");
            return;
        }

        GameMenu modsGameMenu = new MenuFactory()
            .SetObjectName("Location Mods")
            .SetBackButton(settingsGameMenu)
            .SetTitle("MOD SETTINGS")
            .Build();
        
        _modsGameMenu = modsGameMenu;
        
        new MenuOptionFactory()
            .SetName("MODS")
            .SetParent(settingsGameMenu)
            .SetNextLocation(modsGameMenu)
            .PlaceOptionBefore(4)
            .Build();
        
        InitializedEvent.Invoke();
    }

    public static GameMenu GetModMenu(string modName)
    {
        GameMenu gameMenu = MenuManager.Instance.Find($"ModMenu {modName}");
        if(gameMenu != null) return gameMenu;
        
        gameMenu = new MenuFactory()
            .SetObjectName($"ModMenu {modName}")
            .SetTitle(modName)
            .SetBackButton(_modsGameMenu)
            .Build();

        new MenuOptionFactory()
            .SetName(modName)
            .SetParent(_modsGameMenu)
            .SetObjectName($"ModMenuButton {modName}")
            .SetNextLocation(gameMenu)
            .PlaceOptionBefore(gameMenu.MenuOptions.Count - 1)
            .Build();
        
        return gameMenu;
    }
}