namespace TheAether.Gaming.Appearance;

using Rage;
using System.Collections.Generic;

public struct PedConfig
{
    public Dictionary<int, PedVariation> Components { get; set; }
    public PedPersonailty Personailty { get; set; }

    public void Apply(Ped ped)
    {
        foreach (var component in Components)
        {
            ped.SetVariation(component.Key, component.Value.Index, component.Value.Texture);
        }

        Personailty.Apply(ped);
    }
}
