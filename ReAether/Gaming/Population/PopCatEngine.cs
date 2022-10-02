namespace ReAether.Gaming.Population;

using Newtonsoft.Json;
using Rage;
using ReAether.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAether.Gaming;
using TheAether.Gaming.Appearance;

internal class PopCatEngine
{
    private readonly Model FreemodeFemale = new("MP_F_FREEMODE_01");
    private bool ready;
    private static readonly PedOutfit _uniform = JsonConvert.DeserializeObject<PedOutfit>(Resource.CommonsUniform, GameService.SerializerSettings);
    private const float MaxSpeed = 13.88889F; // 50 km/h

    private readonly List<Ped> _poppedPeds = new();
    private readonly List<Vehicle> _poppedVehicles = new();
    private readonly List<Model> _models = new();

    public void Initialise()
    {
        var strings = Resource.PopCatVehicleList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var s in strings)
        {
            var hash = Game.GetHashKey(s);

            if (!Natives.IsModelAVehicle(hash))
            {
                continue;
            }
        }

        FreemodeFemale.LoadAndWait();
        ready = true;
    }

    public void ClearPopulation()
    {
        foreach (var ped in _poppedPeds)
        {
            if (ped?.Exists() == true)
            {
                ped.Delete();
            }
        }

        foreach (var vehicle in _poppedVehicles)
        {
            if (vehicle?.Exists() == true)
            {
                vehicle.Delete();
            }
        }
    }

    internal void AddPedToPopulation(Vector3 pos)
    {
        if (!Natives.GetSafeCoordForPed(pos.X, pos.Y, pos.Z, true, out var p, 0))
        {
            return;
        }

        var ped = new Ped(FreemodeFemale, p, 0);// World.CreatePed(PedHash.FreemodeFemale01, World.GetNextPositionOnSidewalk(pos.Around(82f)));
        PedPersonailty.Random().Apply(ped);
        _uniform.Apply(ped);
        ped.Tasks.Wander();
        _poppedPeds.Add(ped);
    }

    internal void AddVehicleToPopulation(Vector3 pos)
    {
        if (!Natives.GetClosestVehicleNodeWithHeading(pos.X, pos.Y, pos.Z, out var position, out var heading,
            4, 3.0F, 0))
        {
            return;
        }

        var vehicle = new Vehicle(x => x.IsCar
        && !x.IsBus && !x.IsAmphibiousCar && !x.IsOffroadVehicle
        && !x.IsEmergencyVehicle && !x.IsLawEnforcementVehicle
        && !x.IsTrailer && !x.IsVan && !x.EmergencyLighting.Exists(), position, heading);

        var ped = Natives.CreatePedInsideVehicle(vehicle, 0, FreemodeFemale.Hash, -1, false, false);
        PedPersonailty.Random().Apply(ped);
        Natives.SetDriverAbility(ped, 1.0f);
        _uniform.Apply(ped);

        ped.Tasks.CruiseWithVehicle(MaxSpeed, VehicleDrivingFlags.Normal);
        vehicle.IsEngineOn = true;
        Natives.SetVehicleForwardSpeed(vehicle, MaxSpeed);
        _poppedPeds.Add(ped);
        _poppedVehicles.Add(vehicle);
    }

    public void Process()
    {
        var pos = Game.LocalPlayer.Character.Position;

        for (int i = 0; i < _poppedPeds.Count; i++)
        {
            var ped = _poppedPeds[i];

            if (ped?.Exists() != true)
            {
                _poppedPeds.RemoveAt(i);
                continue;
            }

            if (ped.IsDead)
            {
                ped.Dismiss();
                _poppedPeds.RemoveAt(i);
                continue;
            }

            if ((!ped.IsOnScreen && ped.Position.DistanceTo2D(pos) > 105f) || ped.Position.DistanceTo2D(pos) > 275f)
            {
                var vehicle = ped.CurrentVehicle;

                ped.Delete();

                if (vehicle?.Exists() == true && vehicle.Driver?.Exists() != true && vehicle.PassengerCount == 0)
                {
                    _poppedVehicles.Remove(vehicle);
                    vehicle.Delete();
                }

                _poppedPeds.RemoveAt(i);
            }
        }

        for (int i = 0; i < _poppedVehicles.Count; i++)
        {
            var vehicle = _poppedVehicles[i];
            if (!vehicle)
            {
                _poppedVehicles.RemoveAt(i);
                continue;
            }

            if (vehicle.Driver == Game.LocalPlayer.Character)
            {
                vehicle.Dismiss();
                _poppedVehicles.RemoveAt(i);
                continue;
            }

            if (!vehicle.IsPersistent ||
                (vehicle.Driver?.Exists() != true && vehicle.PassengerCount == 0)
                && !vehicle.IsOnScreen)
            {
                vehicle.Delete();
                _poppedVehicles.RemoveAt(i);
            }
        }

        if (_poppedPeds.Count <= 7 && MathHelper.GetRandomInteger(6) > 2)
        {
            if (MathHelper.GetRandomInteger(10) > 3)
            {
                AddVehicleToPopulation(pos.Around(75f, 105f));
            }
            else
            {
                AddPedToPopulation(pos.Around(75f, 105f));
            }
        }
    }
}
