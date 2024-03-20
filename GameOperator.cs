namespace Treblecross
{
    public enum GameMode
    {
        PvE = 0,
        PvP = 1,
    }

    public abstract class GameOperator
    {
        public GameMode Mode { get; }
        private Player[] players = new Player[2];
        private Board board;

        public GameOperator(GameMode mode, Player[] players, Board board)
        {
            Mode = mode;
            if (players.Length != 2)
            {
                throw new InvalidDataException("Must be only 2 players");
            }
            this.players = players;
            this.board = board;
        }

        public void Start()
        {
            // looping players
        }

        void makeMove(Player player)
        {
            // prompting movement question
            // create Gamestate
        }

        protected abstract void makeRandomMove();


    }

    public class TreblecrossOperator : GameOperator
    {
        public TreblecrossOperator(GameMode mode, Player[] players, Board board) : base(mode, players, board) { }

        protected override void makeRandomMove()
        {
            throw new NotImplementedException();
        }
    }

}