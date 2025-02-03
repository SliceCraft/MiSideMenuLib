using MenuLib.API;
using MenuLib.API.Factories;

namespace MenuLib.ModSettings;

public class SettingsManager
{
    internal static void Init()
    {
        API.Menu menu = MenuManager.Instance.Find("Location Menu");
        Plugin.Log.LogInfo(MenuManager.Instance.Menus.Count);
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
        
        new MenuOptionFactory()
            .SetName("Mods")
            .SetParent(menu)
            .SetOnClick(ClickTest)
            .SetNextLocation(settingsMenu.MenuObject)
            .PlaceOptionBefore(3)
            .Build();
    }

    private static void ClickTest()
    {
        Plugin.Log.LogInfo("YOOOO YOU CLICKED, INSANE");
    }
}