using UnityEngine;

namespace MenuLib.API.Factories;

public class MenuFactory
{
    internal static Menu FromExistingMenu(GameObject existingMenu)
    {
        return new Menu(existingMenu);
    }
    
    // TODO: System for creating custom menus
}