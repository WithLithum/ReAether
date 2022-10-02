#define RAGE

namespace TheAether.Gaming;

#if SHVDN
using GTA;
using GTA.Native;
#elif RAGE
using Rage;
using Rage.Native;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
#endif

internal static class Natives
{
    public static bool GetClosestVehicleNode(float x, float y, float z, out Vector3 outPosition, int nodeType, float p5, float p6)
    {
        var result = Vector3.Zero;
        var success = NativeFunction.Natives.x240A18690AE96513<bool>(x, y, z, ref result, nodeType, p5, p6);
        outPosition = result;
        return success;
    }

    public static bool GetRandomVehicleNode(float x, float y, float z, float radius, bool p4, bool p5, bool p6, out Vector3 outPosition, out int nodeId)
    {
        var result = Vector3.Zero;
        var id = 0;

        var success = NativeFunction.Natives.x93E0DB8440B73A7D<bool>(x, y, z, radius, p4, p5, p6, ref result, ref id);
        outPosition = result;
        nodeId = id;

        return success;
    }

    public static bool GetSafeCoordForPed(float x, float y, float z, bool onGround, out Vector3 outPosition, int flags)
    {
        var result = Vector3.Zero;

        var success = NativeFunction.Natives.xB61C8E878A4199CA<bool>(x, y, z, onGround, ref result, flags);
        outPosition = result;
        return success;
    }

    public static bool GetClosestVehicleNodeWithHeading(float x, float y, float z, out Vector3 outPosition, out float heading, int nodeType, float p5, float p6)
    {
        var result = Vector3.Zero;
        var hd = 0f;
        var success = NativeFunction.Natives.xFF071FB798B803B0<bool>(x, y, z, ref result, ref hd, nodeType, p5, p6);
        outPosition = result;
        heading = hd;

        return success;
    }

    public static void SetPedEyeColor(Ped chara, int index)
    {
        NativeFunction.Natives.x50B56988B170AFDF(chara, index);
    }

    public static int GetTimeSinceLastArrest()
    {
        return NativeFunction.Natives.GetTimeSinceLastArrest<int>();
    }

    public static int GetTimeSinceLastDeath()
    {
        return NativeFunction.Natives.GetTimeSinceLastDeath<int>();
    }

    public static void SetPedComponentVariation(Ped ped, int componentId, int drawableId, int textureId, int paletteId)
    {
#if SHVDN
        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, componentId, drawableId, textureId, paletteId);
#elif RAGE
        NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, componentId, drawableId, textureId, paletteId);
#endif
    }

    internal static void TerminateAllScriptsWithThisName(string scriptName)
    {
#if SHVDN
        Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, scriptName);
#elif RAGE
        NativeFunction.Natives.TerminateAllScriptsWithThisName(scriptName);
#endif
    }

    internal static int GetNumHairColors()
    {
#if SHVDN
        return Function.Call<int>(Hash._GET_NUM_HAIR_COLORS);
#elif RAGE
        return NativeFunction.Natives.xE5C0CF872C2AD150<int>();
#endif
    }

    internal static void SetPedHairColor(Ped ped, int colorID, int highlightColorID)
    {
#if SHVDN
        Function.Call(Hash._SET_PED_HAIR_COLOR, ped, colorID, highlightColorID);
#elif RAGE
        NativeFunction.Natives.x4CFFC65454C93A49(ped, colorID, highlightColorID);
#endif
    }

    internal static void SetPedFaceFeature(Ped ped, int index, float scale)
    {
#if SHVDN
        Function.Call(Hash._SET_PED_FACE_FEATURE, ped, index, scale);
#elif RAGE
        NativeFunction.Natives.x71A5C1DBA060049E(ped, index, scale);
#endif
    }

    internal static void SetPedHeadOverlayColor(Ped ped, int overlayID, int colorType, int colorID, int secondColorID)
    {
#if SHVDN
        Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, ped, overlayID, colorType, colorID, secondColorID);
#elif RAGE
        NativeFunction.Natives.x497BF74A7B9CB952(ped, overlayID, colorType, colorID, secondColorID);
#endif
    }

    internal static void SetPedHeadOverlay(Ped ped, int overlayID, int index, float opacity)
    {
#if SHVDN
        Function.Call(Hash.SET_PED_HEAD_OVERLAY, ped, overlayID, index, opacity);
#elif RAGE
        NativeFunction.Natives.x48F44967FA05CC1E(ped, overlayID, index, opacity);
#endif
    }

    internal static Ped CreatePedInsideVehicle(Vehicle vehicle, int pedType, uint modelHash, int seat, bool isNetwork, bool bScriptHostPed)
    {
#if SHVDN
        return Function.Call<Ped>(Hash.CREATE_PED_INSIDE_VEHICLE, vehicle, pedType, modelHash, seat, isNetwork, bScriptHostPed);
#elif RAGE
        return NativeFunction.Natives.x7DD959874C1FD534<Ped>(vehicle, pedType, modelHash, seat, isNetwork, bScriptHostPed);
#endif
    }

    internal static void SetAbilityBarVisiblity(bool visible)
    {
        NativeFunction.Natives.x1DFEDD15019315A9(visible);
    }

    internal static void SetPlayerIsInDirectorMode(bool status)
    {
        NativeFunction.Natives.x808519373FD336A3(status);
    }

    internal static void SetPedPopulationBudget(int budget)
    {
        NativeFunction.Natives.x8C95333CFC3340F3(budget);
    }

    internal static bool IsModelAVehicle(uint model)
    {
        return NativeFunction.Natives.IS_MODEL_A_VEHICLE<bool>(model);
    }

    internal static void SetVehiclePopulationBudget(int budget)
    {
        NativeFunction.Natives.xCB9E1EB3BE2AF4E9(budget);
    }

    internal static void SetDriverAbility(Ped ped, float ability)
    {
        NativeFunction.Natives.xB195FFA8042FC5C3(ped, ability);
    }

    internal static void SetVehicleForwardSpeed(Vehicle vehicle, float speed)
    {
        NativeFunction.Natives.xAB54A438726D25D5(vehicle, speed);
    }

    internal static void RequestModel(uint modelHash)
    {
#if SHVDN
        Function.Call(Hash.REQUEST_MODEL, modelHash);
#elif RAGE
        NativeFunction.Natives.RequestModel(modelHash);
#endif
    }

    internal static void SetHeadBlendData(Ped ped, int shapeFirstID, int shapeSecondID, int shapeThirdID, int skinFirstID, int skinSecondID, int skinThirdID, float shapeMix, float skinMix, float thirdMix, bool isParent)
    {
#if SHVDN
        Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, ped, shapeFirstID,
            shapeSecondID,
            shapeThirdID,
            skinFirstID,
            skinSecondID,
            skinThirdID,
            shapeMix,
            skinMix,
            thirdMix,
            isParent);
#elif RAGE
        NativeFunction.Natives.x9414E18B9434C2FE(ped, shapeFirstID,
            shapeSecondID,
            shapeThirdID,
            skinFirstID,
            skinSecondID,
            skinThirdID,
            shapeMix,
            skinMix,
            thirdMix,
            isParent);
#endif
    }
}
