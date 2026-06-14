using ImGuiNET;

namespace Saucy;

public static unsafe class Extensions
{
    public static bool PassFilterBool(this ImGuiTextFilterPtr self, string text)
    {
        return self.PassFilter(text);
    }
}
