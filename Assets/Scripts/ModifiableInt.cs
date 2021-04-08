using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();

[System.Serializable]
public class ModifiableInt
{
    [SerializeField] private int baseValue;
    public int BaseValue
    {
        get => baseValue;
        set { baseValue = value;
            UpdateModifiedValue();
        }
    }

    [SerializeField] private int modifiedValue;
    public int ModifiedValue
    {
        get => modifiedValue;
        private set => modifiedValue = value;
    }

    public List<IModifier> modifiers = new List<IModifier>();

    public event ModifiedEvent ValueModified;

    public ModifiableInt(ModifiedEvent method = null)
    {
        modifiedValue = BaseValue;
        if (method != null)
            ValueModified += method;
    }

    public void RegisterModEvent(ModifiedEvent method)
    {
        ValueModified += method;
    }
    
    public void UnregisterModEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }

        ModifiedValue = baseValue + valueToAdd;
        if(valueToAdd != null)
            ValueModified.Invoke();
    }

    public void AddModifier(IModifier _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    
    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}
