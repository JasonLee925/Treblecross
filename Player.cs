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

        /// <summary>
        /// Create a computer player
        /// </summary>
        /// <returns>A Player object</returns>
        public static Player CreateComputerPlayer() {
            Piece piece = new Piece('▲', ConsoleColor.Gray);
            return new Player("CPU", PlayerType.Cpu, piece);
        }

        /// <summary>
        /// Create a human player
        /// </summary>
        /// <returns>A Player object</returns>
        public static Player CreateHumanPlayer() {
            char mark  = 'X';
            int colour = 15;
            do {
                Console.WriteLine("[Game] Enter a mark representing player-1? (example: X or O) ");
            } while (!char.TryParse(Console.ReadLine(), out mark));
            do {
                Console.WriteLine("[Game] Enter a color representing player-1? ");
                Console.WriteLine("Options: \r\n" +
                    "0: black\r\n" + "9: blue\r\n" + "10: green\r\n" + "12: red\r\n" + "14: yellow\r\n" + "15: white"
                    );
            } while (!int.TryParse(Console.ReadLine(), out colour));

            Piece peice = new Piece(mark, (ConsoleColor)colour);
            return new Player("CPU", PlayerType.Human, peice);
        }


        // public override string ToString()
        // {
        //     return "[Player] Name: " + Name + ", PlayerType: " + PlayerType + ", Peice: " + Piece.Print();
        // }
    }
    
}