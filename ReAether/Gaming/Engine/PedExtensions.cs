namespace ReAether.Gaming.Engine;

using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TheAether.Gaming;

internal static class PedExtensions
{
    /// <summary>
    /// Set all components of this actor to <c>0</c>.
    /// </summary>
    /// <param name="chara">The actor to set.</param>
    public static void ZeroComponents(this Ped chara)
    {
        for (int i = 0; i < 11; i++)
        {
            chara.SetVariation(i, 0, 0);
        }
    }

    public static void SetFaceFeature(this Ped chara, int index, float feature)
    {
        Natives.SetPedFaceFeature(chara, index, feature);
    }

    public static void SetEyeColour(this Ped chara, int colourIndex)
    {
        Natives.SetPedEyeColor(chara, colourIndex);
    }

    public static void SetHairColour(this Ped chara, int primary, int secondary)
    {
        Natives.SetPedHairColor(chara, primary, secondary);
    }

    public static void SetHeadOverlay(this Ped chara, int slot, int index, float opacity, int primaryColour, int secondaryColour)
    {
        Natives.SetPedHeadOverlay(chara, slot, index, opacity);

        int colourType;

        switch (slot)
        {
            case 1:
            case 2:
            case 10:
                colourType = 1;
                break;
            case 5:
            case 8:
                colourType = 2;
                break;
            default:
                colourType = 0;
                break;
        }

        Natives.SetPedHeadOverlayColor(chara, slot, colourType, primaryColour, secondaryColour);
    }
}
