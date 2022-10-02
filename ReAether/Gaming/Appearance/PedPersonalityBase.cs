namespace TheAether.Gaming.Appearance;

using Rage;

public struct PedPersonalityBase
{
    public int FaceBaseA { get; set; }
    public int FaceBaseB { get; set; }
    public int FaceOverride { get; set; }
    public int ToneBaseA { get; set; }
    public int ToneBaseB { get; set; }
    public int ToneOverride { get; set; }
    public float FaceMix { get; set; }
    public float ToneMix { get; set; }
    public float OverrideMix { get; set; }
    public bool IsParent { get; set; }

    public void Apply(Ped chara)
    {
        Natives.SetHeadBlendData(chara, FaceBaseA,
            FaceBaseB,
            FaceOverride,
            ToneBaseA,
            ToneBaseB,
            ToneOverride,
            FaceMix,
            ToneMix,
            OverrideMix,
            IsParent);
    }
}
