using System;
using System.Collections.Generic;

namespace MenuLib.API.Events;

public static class InitializedEvent
{
    private static readonly List<Action> _actions = new();
    
    public static void AddEventListener(Action listener)
    {
        _actions.Add(listener);
    }

    internal static void Invoke()
    {
        foreach (Action action in _actions)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Plugin.Log.LogError("Encountered the following error while invoking Initialized event listener");
                Plugin.Log.LogError(e);
            }
        }
    }
}