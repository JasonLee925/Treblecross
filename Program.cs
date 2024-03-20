using System.Dynamic;
using System.Globalization;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Treblecross
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Piece piece = new Piece('X', ConsoleColor.DarkGreen);
            Player player = new Player("Kim", PlayerType.Human, piece);


            Console.WriteLine(player.ToString());

            ProgramEngine pe = new ProgramEngine();
            var startNewGame = (StartNewGameDelegate)pe.DisplayMenu();
            GameOperator gameOp = startNewGame();

        }
    }
}
