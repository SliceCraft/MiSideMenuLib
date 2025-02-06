using MenuLib.API;
using MenuLib.API.Factories;

namespace MenuLib.ModSettings;

public class SettingsManager
{
    private static API.Menu _modsMenu = null;
    
    internal static void Init()
    {
        API.Menu menu = MenuManager.Instance.Find("Location Menu");
        if (menu == null)
        {
            Plugin.Log.LogError("Couldn't find the main menu");
            return;            
        }
        
        API.Menu settingsMenu = MenuManager.Instance.Find("Location MainOptions");
        if (settingsMenu == null)
        {
            Plugin.Log.LogError("Couldn't find the settings menu");
            return;
        }

        API.Menu modsMenu = new MenuFactory()
            .SetObjectName("Location Mods")
            .SetBackButton(settingsMenu)
            .SetTitle("MOD SETTINGS")
            .Build();
        
        _modsMenu = modsMenu;
        
        new MenuOptionFactory()
            .SetName("MODS")
            .SetParent(settingsMenu)
            .SetNextLocation(modsMenu)
            .PlaceOptionBefore(4)
            .Build();
    }

    public static API.Menu GetModMenu(string modName)
    {
        API.Menu menu = MenuManager.Instance.Find($"ModMenu {modName}");
        if(menu != null) return menu;
        
        menu = new MenuFactory()
            .SetObjectName($"ModMenu {modName}")
            .SetTitle(modName)
            .SetBackButton(_modsMenu)
            .Build();

        new MenuOptionFactory()
            .SetName(modName)
            .SetParent(_modsMenu)
            .SetObjectName($"ModMenuButton {modName}")
            .SetNextLocation(menu)
            .PlaceOptionBefore(menu.MenuOptions.Count - 1)
            .Build();
        
        return menu;
    }

    private static void ClickTest()
    {
        Plugin.Log.LogInfo("YOOOO YOU CLICKED, INSANE");
    }
}