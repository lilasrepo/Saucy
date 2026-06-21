using FFXIVClientStructs.FFXIV.Client.UI;

namespace Saucy.TripleTriad.UI;

// porting-note(api12/B1): CardIconId / NumSideU/D/R/L / CardType / CardRarity / RequestedPage are 7.5 additions
// *inserted* into AddonGSInfoCardList. Because they shift the struct layout, their 7.5 offsets do NOT map to
// game-7.1 data — reading raw would return garbage. They are therefore stubbed inert: the live card-collection
// navigation / in-game-selection sync feature is non-functional on TC 7.1 (the DB-driven card search window still
// works). TODO(api12): reverse-engineer the 7.1 GSInfoCardList layout to restore live collection navigation.
internal static unsafe class GsCardListCompat
{
    public static int CardIconId(AddonGSInfoCardList* a) => 0;
    public static byte NumSideU(AddonGSInfoCardList* a) => 0;
    public static byte NumSideD(AddonGSInfoCardList* a) => 0;
    public static byte NumSideR(AddonGSInfoCardList* a) => 0;
    public static byte NumSideL(AddonGSInfoCardList* a) => 0;
    public static byte CardType(AddonGSInfoCardList* a) => 0;
    public static byte CardRarity(AddonGSInfoCardList* a) => 0;
    public static void SetRequestedPage(AddonGSInfoCardList* a, int value) { /* inert — see note above */ }
}
