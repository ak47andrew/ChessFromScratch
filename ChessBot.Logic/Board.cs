namespace ChessBot.Logic;

public class Board
{
    static string STARTPOS = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public byte[] board { get; private set; }
    public bool isWhiteTurn { get; private set; }
    public byte castlingRights { get; private set; } // Castling 0bKQkq
    public Square? enPassantTarget { get; private set; }
    public int halfMoveClock { get; private set; }
    public int fullMoveNumber { get; private set; }

    public Board(
        byte[] board,
        bool? isWhiteTurn,
        byte? castlingRights,
        Square? enPassantTarget,
        int? halfMoveClock,
        int? fullMoveNumber
    )
    {
        this.board = board;
        this.isWhiteTurn = isWhiteTurn ?? true;
        this.castlingRights = castlingRights ?? 0b1111;
        this.halfMoveClock = halfMoveClock ?? 0;
        this.fullMoveNumber = fullMoveNumber ?? 1;
        this.enPassantTarget = enPassantTarget;
    }

    public Board(string fen = "")
    {
        board = new byte[64];
        if (fen == "")
        {
            fen = STARTPOS;
        }
        _loadFromFen(fen);
    }

    public void SetPiece(Square square, byte piece)
    {
        board[square.Index] = piece;
    }

    private void _loadFromFen(string fen)
    {
        string pieceString = fen.Split(' ')[0];
        int rank = 0;
        int file = 0;

        foreach (char c in pieceString)
        {
            if (char.IsDigit(c))
            {
                file += (byte)(c - '0');
            }
            else if (c == '/')
            {
                rank++;
                file = 0;
            }
            else
            {
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
                    (byte)(
                        pt | (c.ToString().ToLower() == c.ToString() ? Piece.Black : Piece.White)
                    )
                );
                Console.WriteLine($"Added piece: {c} at rank={rank}, file={file}");
                file++;
            }
        }
    }
}
