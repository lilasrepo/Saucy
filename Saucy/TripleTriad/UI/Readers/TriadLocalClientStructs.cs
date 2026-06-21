using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static ECommons.GenericHelpers;

namespace Saucy.TripleTriad.UI;

internal static unsafe class TriadLocalClientStructs
{
    public static bool TryGetRequest(out AddonRequest* addon, bool requireVisible = true) =>
        TryGetVisible("TripleTriadRequest", out addon, requireVisible);

    public static bool TryGetSelDeck(out AddonTripleTriadSelDeck* addon, bool requireVisible = true) =>
        TryGetVisible("TripleTriadSelDeck", out addon, requireVisible);

    public static bool TryGetResult(out AddonTripleTriadResult* addon, bool requireVisible = true) =>
        TryGetVisible("TripleTriadResult", out addon, requireVisible);

    public static bool TryGetBoard(out AddonTripleTriad* addon, bool requireVisible = true)
    {
        if (!TryGetAddonByName("TripleTriad", out addon))
        {
            return false;
        }

        return !requireVisible || addon->AtkUnitBase.IsVisible;
    }

    private static bool TryGetVisible<T>(string addonName, out T* addon, bool requireVisible)
    where T : unmanaged
    {
        if (!TryGetAddonByName(addonName, out addon))
        {
            return false;
        }

        if (!requireVisible)
        {
            return true;
        }

        return ((AtkUnitBase*)addon)->IsVisible;
    }
}

[StructLayout(LayoutKind.Explicit, Size = 0x1D0)]
internal unsafe struct AgentTripleTriad
{
    [FieldOffset(0x00)] public AgentInterface AgentInterface;
    [FieldOffset(0x1C8)] public uint RewardItemId;

    internal static AgentTripleTriad* TryGet()
    {
        var module = AgentModule.Instance();
        if (module == null)
        {
            return null;
        }

        return (AgentTripleTriad*)module->GetAgentByInternalId((AgentId)174); // porting-note(api12): AgentId.TripleTriad (=174 in FCS 7.5) not a named member in API12 FCS
    }
}

[StructLayout(LayoutKind.Explicit)]
internal struct AddonTripleTriadSelDeck
{
    [FieldOffset(0)] public AtkUnitBase AtkUnitBase;
}

[StructLayout(LayoutKind.Explicit)]
internal struct AddonTripleTriadResult
{
    [FieldOffset(0)] public AtkUnitBase AtkUnitBase;
}

// porting-note(api12/C): FCS 7.5 ships a generated FFXIVClientStructs.FFXIV.Client.UI.AddonTripleTriad,
// absent from the API12 (game-7.1) FCS reference. Mirror it locally with the upstream 7.5 field offsets.
// Triple Triad is the same Dawntrail expansion on 7.1 and 7.5, so the addon layout is very likely identical,
// but offsets are RUNTIME-PENDING — verify board/deck reads on the TC client before trusting auto-play.
// porting-note(api12/C): FCS 7.5 ships this enum next to AddonTripleTriad; mirror it locally for API12.
internal enum TurnState : byte
{
    Waiting = 0,
    NormalMove = 1,
    MaskedMove = 2, // Order/Chaos
}

[StructLayout(LayoutKind.Explicit, Size = 0xFD8)]
internal unsafe struct AddonTripleTriad
{
    [FieldOffset(0)] public AtkUnitBase AtkUnitBase;
    [FieldOffset(0x238)] public TurnState TurnState;

    public TripleTriadCard* BlueDeck => (TripleTriadCard*)((byte*)Unsafe.AsPointer(ref this) + 0x240);
    public TripleTriadCard* RedDeck => (TripleTriadCard*)((byte*)Unsafe.AsPointer(ref this) + 0x588);
    public TripleTriadCard* Board => (TripleTriadCard*)((byte*)Unsafe.AsPointer(ref this) + 0x8D0);

    [StructLayout(LayoutKind.Explicit, Size = 0xA8)]
    public unsafe struct TripleTriadCard
    {
        [FieldOffset(0x8)] public AtkComponentBase* CardDropControl;
        [FieldOffset(0x80)] public byte CardRarity; // 1..5
        [FieldOffset(0x81)] public byte CardType;
        [FieldOffset(0x82)] public byte CardOwner;  // 0 empty, 1 blue, 2 red
        [FieldOffset(0x83)] public byte NumSideU;
        [FieldOffset(0x84)] public byte NumSideD;
        [FieldOffset(0x85)] public byte NumSideR;
        [FieldOffset(0x86)] public byte NumSideL;
        [FieldOffset(0xA4)] public bool HasCard;
    }
}
