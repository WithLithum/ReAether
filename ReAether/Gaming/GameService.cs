namespace ReAether.Gaming;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rage;
using ReAether.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheAether.Gaming;

internal static class GameService
{
    internal static int MaximumHairColours { get; private set; }
    internal static int TimeSinceLastArrest => Natives.GetTimeSinceLastArrest();
    internal static int TimeSinceLastDeath => Natives.GetTimeSinceLastDeath();

    internal static readonly JsonSerializerSettings SerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };

    internal static void Initialize()
    {
        MaximumHairColours = Natives.GetNumHairColors();
        SetAbilityBarVisible(false);
        Natives.SetPlayerIsInDirectorMode(true);
        Natives.SetPedPopulationBudget(0);
        Natives.SetVehiclePopulationBudget(0);
    }

    internal static void SetAbilityBarVisible(bool visible)
    {
        Natives.SetAbilityBarVisiblity(visible);
    }

    internal static void KillVanillaScripts()
    {
        var list = Resource.Scripts;

        var lines = list.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            Game.TerminateAllScriptsWithName(line);
        }
    }
}
