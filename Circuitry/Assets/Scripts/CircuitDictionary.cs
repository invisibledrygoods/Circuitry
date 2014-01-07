using UnityEngine;
using System.Collections.Generic;
using Require;
using System;

[Serializable]
public class CircuitDictionaryItem
{
    public string transitionName;
    public List<CircuitComponent> transition;
}

public class CircuitDictionaryComponent : CircuitComponent
{
    public List<CircuitDictionaryItem> transitions;

    public void Spark(string transitionName)
    {
        foreach (CircuitDictionaryItem item in transitions)
        {
            if (transitionName == item.transitionName)
            {
                Spark(item.transition);
            }
        }
    }

    public void Chain(string transitionName, CircuitComponent component)
    {
        if (transitions == null)
        {
            transitions = new List<CircuitDictionaryItem>();
        }

        foreach (CircuitDictionaryItem item in transitions)
        {
            if (item.transitionName == transitionName)
            {
                if (item.transition == null)
                {
                    item.transition = new List<CircuitComponent>();
                }

                item.transition.Add(component);

                return;
            }
        }

        CircuitDictionaryItem newItem = new CircuitDictionaryItem();

        newItem.transitionName = transitionName;
        newItem.transition = new List<CircuitComponent>();
        newItem.transition.Add(component);

        transitions.Add(newItem);
    }
}
