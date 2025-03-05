namespace ChessBot.Logic;

public class Board {
    public byte[] board {get; private set;}
    static string STARTPOS = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public Board(string fen = "") {
        board = new byte[64];
        if (fen == "") {
            fen = STARTPOS;
        }
        _loadFromFen(fen);
    }

    public void SetPiece(Square square, byte piece) {
        board[square.Index] = piece;
    }

    private void _loadFromFen(string fen) {
        string pieceString = fen.Split(' ')[0];
        int rank = 0;
        int file = 0;

        foreach (char c in pieceString)
        {
            if (char.IsDigit(c)) {
                file += (byte)(c - '0');
            } else if (c == '/') {
                rank++;
                file = 0;
            } else {
                byte pt = 0;

                pt = c.ToString().ToLower() switch
                {
                    "p" => Piece.Pawn,
                    "b" => Piece.Bishop,
                    "n" => Piece.Knight,
                    "r" => Piece.Rook,
                    "q" => Piece.Queen,
                    "k" => Piece.King,
                    _ => Piece.None,
                };

                SetPiece(
                    new Square(rank, file), 
                    (byte)(pt | (c.ToString().ToLower() == c.ToString() ? Piece.Black : Piece.White))
                );
                Console.WriteLine($"Added piece: {c} at rank={rank}, file={file}");
                file++;
            }
        }
    }
}