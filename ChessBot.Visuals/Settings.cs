using Raylib_cs;

namespace ChessBot.Visuals;

static class Settings {
    public const int ScreenWidth = 1280;
    public const int ScreenHeight = 720;
    public const int CellSize = 85;

    public static readonly Color BackgroundColor = new Color(84, 84, 84);
    public static readonly Color CellColorDark = new Color(118, 150, 86);
    public static readonly Color CellColorLight = new Color(238, 238, 210);
}
