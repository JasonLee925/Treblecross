namespace Treblecross
{
    public class Piece
    {

        public readonly char Mark = 'X'; // default: X
        public readonly ConsoleColor Colour = ConsoleColor.White; // default: white

        public string Label => Mark.ToString() + ((int)Colour).ToString();

        public Piece(char mark)
        {
            Mark = mark;
        }

        public Piece(char mark, ConsoleColor colour)
        {
            Mark = mark;
            Colour = colour;
        }

        public string Print()
        {
            // ANSI code
            return $"\u001b[38;5;{(int)Colour}m{Mark}\u001b[0m";
        }

        public override string ToString()
        {
            return "[Piece] Mark: " + Print() + " Label: " + Label;
        }
    }

    public enum PlayerType
    {
        Cpu = 0,
        Human = 1,
    }

    public class Player
    {
        public string Name { get; }
        public PlayerType PlayerType { get; }
        public Piece Piece { get; }

        public Player(string name, PlayerType playerType, Piece piece)
        {
            Name = name;
            PlayerType = playerType;
            if (PlayerType == PlayerType.Cpu)
            {
                Name += "(cpu)";
            }

            Piece = piece;
        }

        public override string ToString()
        {
            return "[Player] Name: " + Name + ", PlayerType: " + PlayerType + ", Peice: " + Piece.Print();
        }
    }
    
}