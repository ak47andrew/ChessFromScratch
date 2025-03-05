namespace ChessBot.Logic;

public struct Square
{
    private byte value;

    public int Index => value;
    public byte File => (byte)(value / 8);
    public byte Rank => (byte)(value % 8);

    public Square(byte index)
    {
        value = index;
    }

    public Square(int file, int rank)
    {
        value = (byte)(file * 8 + rank);
    }

    public static Square None => new Square(0);
}
