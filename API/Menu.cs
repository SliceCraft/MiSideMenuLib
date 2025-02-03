using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MenuLib.API;

public class Menu
{
    public GameObject MenuObject { get; }

    internal Menu(GameObject originalMenu)
    {
        MenuObject = originalMenu;
    }

    private GameObject FindTitle()
    {
        Transform transform = MenuObject.transform.Find("Text");
        if(transform == null) return null;
        return transform.gameObject;
    }

    public string Title
    {
        get
        {
            GameObject go = FindTitle();
            if (go == null) return "";
            Text text = go.GetComponent<Text>();
            return text == null ? "" : text.text;
        }
        set => FindTitle().GetComponent<Text>().text = value;
    }

    private List<MenuOption> GetMenuOptions()
    {
        List<MenuOption> menuOptions = [];

        for (int i = 0; i < MenuObject.transform.childCount; i++)
        {
            GameObject child = MenuObject.transform.GetChild(i).gameObject;
            // By checking if the child has a MenuNextLocation or ButtonMouseClick
            // we can verify if it's a button or something else.
            if (child.GetComponent<MenuNextLocation>() == null &&
                child.GetComponent<ButtonMouseClick>() == null) continue;

            menuOptions.Add(new MenuOption(child));
        }
        
        return menuOptions.OrderBy(e => -1 * e.OptionObject.transform.localPosition.y).ToList();
    }

    public List<MenuOption> MenuOptions => GetMenuOptions();

    // TODO: Method for forcing to switch to this menu
    // TODO: System for modifying existing menus
}