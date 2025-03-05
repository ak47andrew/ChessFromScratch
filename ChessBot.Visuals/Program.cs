using ChessBot.Logic;
using Raylib_cs;
using System.Numerics;

namespace ChessBot.Visuals;

class Program {
    static void DrawBoard(byte[] pieces)
    {
        int StartposX = (Settings.ScreenWidth - 8 * Settings.CellSize) / 2;
        int StartposY = (Settings.ScreenHeight - 8 * Settings.CellSize) / 2;

        float fontSize = Settings.FontBaseSize * (Settings.CellSize / 64f); // 64 = base cell size
        float padding = Settings.CellSize * 0.05f;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int posX = StartposX + x * Settings.CellSize;
                int posY = StartposY + y * Settings.CellSize;

                // Draw the chessboard cell
                Color cellColor = (x + y) % 2 == 0 ? Settings.CellColorDark : Settings.CellColorLight;
                Raylib.DrawRectangle(posX, posY, Settings.CellSize, Settings.CellSize, cellColor);

                // Draw coordinates on the leftmost column (ranks 1-8)
                if (x == 0)
                {
                    int rank = 8 - y;
                    string rankStr = rank.ToString();
                    Color textColor = ((x + y) % 2 == 0) ? Settings.CellColorLight : Settings.CellColorDark;

                    Vector2 position = new Vector2(posX + padding, posY + padding);
                    Raylib.DrawTextPro(
                        Settings.BoardFont,
                        rankStr,
                        position,
                        Vector2.Zero,
                        (float)(Math.PI / 2),
                        fontSize,
                        0,
                        textColor
                    );
                }

                // Draw coordinates on the bottom row (files a-h)
                if (y == 7)
                {
                    char file = (char)('a' + x);
                    string fileStr = file.ToString();
                    Color textColor = ((x + y) % 2 == 0) ? Settings.CellColorLight : Settings.CellColorDark;

                    Vector2 textSize = Raylib.MeasureTextEx(Settings.BoardFont, fileStr, fontSize, 0);
                    float textX = posX + Settings.CellSize - textSize.X - padding;
                    float textY = posY + Settings.CellSize - textSize.Y - padding;

                    Raylib.DrawTextEx(
                        Settings.BoardFont,
                        fileStr,
                        new Vector2(textX, textY),
                        fontSize,
                        0,
                        textColor
                    );
                }

                // Draw piece on the cell
                byte piece = pieces[x + y * 8];
                if ((0b111 & piece) == 0) {
                    continue;
                }

                Texture2D texture = Settings.pieces[piece];
                Raylib.DrawTexture(texture, posX, posY, Color.White);
            }
        }
    }

    static void PreLoad(){
        Settings.BoardFont = Raylib.LoadFont("resources/NotoSans.ttf");
        Raylib.SetTextureFilter(Settings.BoardFont.Texture, TextureFilter.Bilinear);

        Settings.pieces[Piece.White | Piece.Pawn] = LoadPiece("wp");
        Settings.pieces[Piece.White | Piece.Knight] = LoadPiece("wn");
        Settings.pieces[Piece.White | Piece.Bishop] = LoadPiece("wb");
        Settings.pieces[Piece.White | Piece.Rook] = LoadPiece("wr");
        Settings.pieces[Piece.White | Piece.Queen] = LoadPiece("wq");
        Settings.pieces[Piece.White | Piece.King] = LoadPiece("wk");

        Settings.pieces[Piece.Black | Piece.Pawn] = LoadPiece("bp");
        Settings.pieces[Piece.Black | Piece.Knight] = LoadPiece("bn");
        Settings.pieces[Piece.Black | Piece.Bishop] = LoadPiece("bb");
        Settings.pieces[Piece.Black | Piece.Rook] = LoadPiece("br");
        Settings.pieces[Piece.Black | Piece.Queen] = LoadPiece("bq");
        Settings.pieces[Piece.Black | Piece.King] = LoadPiece("bk");
    }

    static Texture2D LoadPiece(string name)
    {
        Image original = default;
        Image resized = default;
        
        try
        {
            original = Raylib.LoadImage($"resources/piece/{name}.png");
            resized = Raylib.ImageCopy(original);
            Raylib.ImageResize(ref resized, Settings.CellSize, Settings.CellSize);
            
            Texture2D texture = Raylib.LoadTextureFromImage(resized);
            Raylib.SetTextureFilter(texture, TextureFilter.Bilinear);
            
            // Important: Flush the GPU pipeline
            Raylib.BeginDrawing();
            Raylib.EndDrawing();
            
            return texture;
        }
        finally
        {
            Raylib.UnloadImage(original);
            Raylib.UnloadImage(resized);
        }
    }

    public static void Main(){
        Raylib.InitWindow(1280, 720, "ChessBot");

        PreLoad();

        Board board = new Board();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Settings.BackgroundColor);

            DrawBoard(board.board);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }
}
