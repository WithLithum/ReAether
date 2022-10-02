namespace TheAether.Gaming.Appearance;

using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct PedOutfit
{
    public Dictionary<int, PedVariation> Components { get; set; }

    public void Apply(Ped ped)
    {
        foreach (var component in Components)
        {
            ped.SetVariation(component.Key, component.Value.Index, component.Value.Texture);
        }
    }
}
