using ImGuiNET;
namespace Saucy;

public static unsafe class ImGuiExtensions
{
    // porting-note(api12): the new ImGui binding's ImU8String/self.Handle/ImGuiNative.PassFilter(ptr,ptr,ptr)
    // overload doesn't exist in ImGui.NET (API12). ImGuiTextFilterPtr.PassFilter(string) is the native equivalent.
    public static bool PassFilterBool(this ImGuiTextFilterPtr self, string text)
        => self.PassFilter(text);
}
