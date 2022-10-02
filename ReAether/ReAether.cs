[assembly: Rage.Attributes.Plugin("ReAether", Author = "TheAetherProject", Description = "The Aether",
    EntryPoint = "ReAether.ReAetherPlugin.Entry", ExitPoint = "ReAether.ReAetherPlugin.Exit")]

namespace ReAether;

using Rage;
using ReAether.Gaming;
using ReAether.Gaming.Population;
using System;

public class ReAetherPlugin
{
    public static ReAetherPlugin Instance { get; private set; }
    public bool Ready { get; private set; }
    private GameFiber serviceWorker;
    private readonly PopCatEngine engine = new();
    private bool stop;

    public void Initialize()
    {
        while (Game.IsLoading)
        {
            GameFiber.Yield();
        }

        GameService.KillVanillaScripts();
        GameService.Initialize();

        engine.Initialise();
        serviceWorker = GameFiber.StartNew(() =>
        {
            while (!stop)
            {
                GameFiber.Yield();
                engine.Process();
            }
        });
        Ready = true;
    }

    public static void Entry()
    {
        Instance = new ReAetherPlugin();
        Instance.Initialize();

        while (!Instance.stop)
        {
            GameFiber.Yield();
        }
    }

    public void Unload()
    {
        stop = true;
        engine.ClearPopulation();
    }

    public static void Exit(bool forced)
    {
        Instance.Unload();
    }
}
