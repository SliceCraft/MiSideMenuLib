using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MenuLib.API;

public class GameMenu
{
    public GameObject MenuObject { get; }

    internal GameMenu(GameObject originalMenu)
    {
        MenuObject = originalMenu;
    }

    private GameObject FindTitle()
    {
        Transform transform = MenuObject.transform.Find("Text");
        return transform == null ? null : transform.gameObject;
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
    
    public Text TextComponent => FindTitle().GetComponentInChildren<Text>();

    private List<MenuOption> GetMenuOptions()
    {
        List<MenuOption> menuOptions = [];

        for (int i = 0; i < MenuObject.transform.childCount; i++)
        {
            GameObject child = MenuObject.transform.GetChild(i).gameObject;
            // We assume every item in the menu except the title (which has a Text component)
            // is an option.
            // The menu divider is interpreted as a MenuOption, the MenuOption handles the rest.
            if (child.GetComponent<Text>() != null) continue;

            menuOptions.Add(new MenuOption(child));
        }
        
        return menuOptions.OrderBy(e => -1 * e.OptionObject.transform.localPosition.y).ToList();
    }

    public List<MenuOption> MenuOptions => GetMenuOptions();
    
    public MenuOption Find(string name)
    {
        return MenuOptions.FirstOrDefault(option => option.OptionObject.gameObject.name.ToLower().Equals(name.ToLower()), null);
    }

    // TODO: Method for forcing to switch to this menu
}