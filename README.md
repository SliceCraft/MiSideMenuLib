# MiSide MenuLib
This library aims to help implementing custom menu options in the main menu of the MiSide game.  
This library only works for BepInEx.

# Features
- Create custom menus and menu buttons
- Edit existing menus and menu buttons
- Modify or add navigation to different menus and run code when a button is pressed
- Menu dividers

# How to implement
Currently this mod is only available through this Github repository.  
You can download the dll file and add it as a reference to your project while developing.  
When you compile your mod copy both your own mod and the menulib to the bepinex plugin folder.  
Your mod should now be able to work with the menulib.

# API
## Events
There are a few events so you can run code in certain scenarios.

### InitializedEvent
This event is invoked after the main menu scene has been loaded when the library has been fully setup and is ready to be used.  
It's recommended that you add your menu options after this event has been invoked.  
To register to this event you can call the method in `InitializedEvent#AddEventListener(Action listener)`.

### InitializedMenuManagerEvent
This event is invoked right after the MenuManager has initialized the cache.
To register to this event you can call the method in `InitializedMenuManagerEvent#AddEventListener(Action listener)`.
#### IMPORTATNT
It's recommended to use `InitializedEvent` instead of this event since the full library won't be ready yet when this event is invoked.

## MenuManager
The MenuManager is using the singleton pattern and can be accessed using `MenuManager.Instance`.

### Menus
The `Menus` field will return all currently created menus.

### Find(string name)
The `Find` method searches for a menu with the provided name.  
The name is not case sensitive but duplicate names will cause the first found object to be returned.  

## SettingsManager
The settings manager creates a menu for every mod that is registered through this manager.

### GetModMenu(string modName)
This method will return a GameMenu instance for your mod which will be created based on the name provided.

## GameMenu
The GameMenu object contains information about a specific menu.

### Fields
- Title: Title of the menu
- TextComponent: The text component of the Title
- MenuOptions: A list of the menu options this menu has

### Find(string name)
The `Find` method searches for a menu option with the provided name.  
The name is not case sensitive but duplicate names will cause the first found object to be returned.

## MenuOption
The MenuOption object contains information about a specific menu option.

### Fields
- Text: Text on the button
- TextComponent: The text component of the button
- NextLocation: The GameObject of the menu that will be switched to when pressing this button
- OnClick: The UnityEvent that will be invoked when the button is pressed

### IsDivider()
Returns whether this menu option is a divider.  
It's not intended to modify existing dividers.

## MenuFactory
This factory can create new GameMenus, the methods in this factory can be chained.  
Example:
```C#
new MenuFactory()
    .SetObjectName("Cool Menu")
    .SetBackButton(previousMenu)
    .SetTitle("BUTTON")
    .Build();
```

### SetObjectName(string objectName)
Set the name of the GameObject that will be used for this menu.

### SetBackButton(GameMenu gotoGameMenu)
Enables the back button, the back button will open the menu specified in the `gotoGameMenu` parameter.

### SetTitle(string title)
Set the title of the menu.

### Build()
Create the menu with the specified settings and return the GameMenu instance.  
The title is a required setting that needs to be set before calling the Build method.

## MenuOptionFactory
This factory can create new MenuOptions, the methods in this factory can be chained.  
Example:
```C#
new MenuOptionFactory()
    .SetName("START OF GAME")
    .SetParent(menu)
    .PlaceOptionBefore(menu.MenuOptions.Count - 1)
    .SetNextLocation(startOfGameMenu) 
    .Build();
```

### SetName(string name)
Set the text that will be displayed on the button.

### SetNextLocation(GameMenu nextLocation)
Set what GameMenu will be opened when the button is pressed.

### SetOnClick(Action unityAction)
Set the action that should be invoked when the button has been clicked.

### SetParent(GameMenu parent)
Set what GameMenu this button is part of.

### PlaceOptionBefore(int placeBefore)
Change before what button this button should be placed.  
When this option isn't defined or -1 is specified the button will be added to the end.  
This option has to be undefined or -1 if there are no existing buttons in the menu.

### PlaceOptionBefore(MenuOption placeBefore)
This method hasn't been implemented yet and will return null.

### SetObjectName(string name
Set the name of the GameObject that will be used for this menu option.

### Build()
This method will build the menu option based on the settings provided in the other method calls.  
The return value is the instantiated MenuOption.  
The name, parent and the next location are required settings that need to be set before calling the Build method.

### BuildMenuDivider()
This method will build a menu divider based on the settings provided in the other method calls.  
The return value is the instantiated MenuOption.  
The parent is a required setting that needs to be set before calling the BuildMenuDivider method.  
Any other parameters other than parent and place before are ignored.