using ChessBot.Logic;
using Raylib_cs;

namespace ChessBot.Visuals;

class Program {
    static void DrawBoard(Piece[] pieces){
        int StartposX = (Settings.ScreenWidth - 8 * Settings.CellSize) / 2;
        int StartposY = (Settings.ScreenHeight - 8 * Settings.CellSize) / 2;

        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                int posX = StartposX + x * Settings.CellSize;
                int posY = StartposY + y * Settings.CellSize;

                Raylib.DrawRectangle(posX, posY, Settings.CellSize, Settings.CellSize, 
                                (x + y) % 2 == 0 ? Settings.CellColorDark : Settings.CellColorLight);
            }
        }
    }

    public static void Main(String[] args){
        Raylib.InitWindow(1280, 720, "ChessBot");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Settings.BackgroundColor);

            DrawBoard(new Piece[1]);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }
}
