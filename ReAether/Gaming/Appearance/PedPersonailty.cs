namespace TheAether.Gaming.Appearance;

using Rage;
using ReAether.Gaming;
using ReAether.Gaming.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public struct PedPersonailty
{
    public PedPersonalityBase Bases { get; set; }
    public PedPersonailtyColour HairColour { get; set; }
    public int EyeColour { get; set; }
    public List<float> FacialFeatures { get; set; }
    public Dictionary<int, PedPersonailtyOverlay> Overlays { get; set; }

    public static PedPersonailty Random()
    {
        var result = new PedPersonailty
        {
            FacialFeatures = new List<float>(),
            Overlays = new Dictionary<int, PedPersonailtyOverlay>(),
            EyeColour = 1,
            HairColour = new PedPersonailtyColour()
            {
                Primary = MathHelper.GetRandomInteger(1, GameService.MaximumHairColours),
                Secondary = MathHelper.GetRandomInteger(1, GameService.MaximumHairColours)
            },

            Bases = new PedPersonalityBase()
            {
                FaceBaseA = MathHelper.GetRandomInteger(1, 30),
                FaceBaseB = MathHelper.GetRandomInteger(1, 30),
                FaceOverride = 0,
                FaceMix = MathHelper.GetRandomSingle(-1.0f, 1.0f),
                ToneBaseA = 1,
                ToneBaseB = 1,
                ToneMix = 1f,
                IsParent = false,
                OverrideMix = 0f,
                ToneOverride = 1
            }
        };

        for (int i = 0; i < 19; i++)
        {
            result.FacialFeatures.Add(MathHelper.GetRandomSingle(-1, 1));
        }

        result.Overlays.Add(8, new PedPersonailtyOverlay()
        {
            Colour = 2,
            Index = 1,
            ColourSecondary = 0,
            Opacity = 1
        });

        return result;
    }

    public void Apply(Ped chara)
    {
        Bases.Apply(chara);
        chara.SetHairColour(HairColour.Primary, HairColour.Secondary);

        for (int i = 0; i < 19; i++)
        {
            chara.SetFaceFeature(i, FacialFeatures[i]);
        }

        foreach (var overlay in Overlays)
        {
            chara.SetHeadOverlay(overlay.Key, overlay.Value.Index,
                overlay.Value.Opacity,
                overlay.Value.Colour,
                overlay.Value.ColourSecondary);
        }
    }
}
