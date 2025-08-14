using System.Runtime.InteropServices;
namespace VolumeControlAnywhere.Utils;

public class GlobalHotkey
{
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public const int ModNone = 0x0000;
    public const int ModAlt = 0x0001;
    public const int ModControl = 0x0002;
    public const int ModShift = 0x0004;
    public const int ModWin = 0x0008;

    public const int WmHotkey = 0x0312;

    public const int HotkeyId = 1;
}