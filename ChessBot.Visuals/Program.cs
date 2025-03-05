using ChessBot.Logic;
using Raylib_cs;

namespace ChessBot.Visuals;

class Program {
    static void DrawBoard(Piece[] pieces){
        // Startpos: (560, 280); 20 pixels
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                int posX = 300 + x * 85;
                int posY = 20 + y * 85;
                Color color;

                if ((x + y) % 2 == 0) {
                    color = Color.White;
                } else {
                    color = Color.Gray;
                }

                Raylib.DrawRectangle(posX, posY, 85, 85, color);
            }
        }
    }

    public static void Main(String[] args){
        Raylib.InitWindow(1280, 720, "ChessBot");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(84, 84, 84));

            DrawBoard(new Piece[1]);

            // Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }
}
