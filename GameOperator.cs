using System.Data.Common;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;

namespace Treblecross
{
    public enum GameMode
    {
        None = -1, // temporary
        PvE = 0,
        PvP = 1,
    }

    public abstract class GameOperator
    {
        public abstract string GameId {get;}
        public GameMode Mode { get; }
        private Player[] players = new Player[2];
        private Board board;

        public GameOperator() {
            int mode = -1;
            bool valid = false;
            do {
                Console.WriteLine("[Game] Choose mode? (0: PvE, 1: PvP) ");
                valid = int.TryParse(Console.ReadLine(), out mode);
                if (!Enum.IsDefined(typeof(GameMode), mode) || !valid) {
                    Console.WriteLine("Wrong GameMode, please re-enter!");
                    valid = false;
                }
            } while(!valid);
            Mode = (GameMode) mode;
        }

        public GameOperator(GameMode mode, Player[] players, Board board)
        {
            Mode = mode;
            if (players.Length != 2)
            {
                throw new InvalidDataException("Must be only 2 players");
            }
            this.players = players;
            this.board = board;

            // x, y = Readline();
            
            // update2DArray = validateMove(x, y)
            // Gamestate state = new Gamestate(update2DArray) // create Gamestate
            // updateBoard(state);
        }

        protected void init() {
            // create players
            if (Mode == GameMode.PvE) {
                players.SetValue(Player.CreateComputerPlayer(), 0); // a white 'X'
                players.SetValue(Player.CreateHumanPlayer(), 1);
            } else { 
                // PvP
                players.SetValue(Player.CreateHumanPlayer(), 0);
                players.SetValue(Player.CreateHumanPlayer(), 1);
            }

            // prompt "board size"
            int dx = 10;
            do {
                Console.WriteLine("[Game] What is your board size? (Enter a number that is <=5) ");
            } while(!int.TryParse(Console.ReadLine(), out dx) || dx <= 5);
            board = new Board(dx,1);
        }

        public void Start()
        {
            // looping players
        }

        void makeMove(Player player)
        {
            // impletement template pattern.

            // prompting movement question
            // create Gamestate
        }

        protected abstract void makeRandomMove();
        protected abstract void validateMove(GameState state);
        protected abstract void updateBoard(GameState state);
        protected abstract void determineWinner(GameState state);
        protected abstract void end();
        public abstract void GetHint();
    }

    public class TreblecrossOperator : GameOperator
    {
        public override string GameId => "Treblecross";

        public TreblecrossOperator() {
            Console.WriteLine("Creating Treblecross ... ");
            base.init();
        }

        public TreblecrossOperator(GameMode mode, Player[] players, Board board) : base(mode, players, board) { }


        public override void GetHint()
        {
            throw new NotImplementedException();
        }

        protected override void determineWinner(GameState state)
        {
            throw new NotImplementedException();
        }

        protected override void end()
        {
            throw new NotImplementedException();
        }

        protected override void makeRandomMove()
        {
            throw new NotImplementedException();
        }

        protected override void updateBoard(GameState state)
        {
            throw new NotImplementedException();
        }

        protected override void validateMove(GameState state)
        {
            throw new NotImplementedException();
        }
    }


    // public class ReversiOperator : GameOperator {}
}