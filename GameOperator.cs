using System.Reflection.Metadata;

namespace Treblecross
{
    public enum GameMode
    {
        PvE = 0,
        PvP = 1,
    }

    public abstract class GameOperator
    {
        public abstract string GameId {get;}
        public GameMode Mode { get; }
        private Player[] players = new Player[2];
        private Board board;

        public GameOperator(GameMode mode) {
            Mode = mode;
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

        public void Init() {
            // create players
            // prompt "board size"
            // create board
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

}