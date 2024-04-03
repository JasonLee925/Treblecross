using System.Reflection.Metadata.Ecma335;

namespace Treblecross
{
    
    public class GameState {

        public int[,] State;
        public Player Player;
        GameStateHistory gameStateHistory = GameStateHistory.Instance;

        public GameState(int[,] state)
        {
            State = state;
        }
        
        public GameState(Player player, int[,] state)
        {
            Player = player;
            State = state;
        }

        public void Redo()
        {
            GameState currentState = gameStateHistory.GetCurrentState();
            if (currentState.State != State)
                gameStateHistory.GetNext();
            else
                Console.WriteLine("You are on the most updated move.");
        }

        public void Undo()
        {
            gameStateHistory.GetPrevious();
        }
    }


    //singleton class
    public class GameStateHistory
    {
        private static GameStateHistory _instance;
        private List<GameState> StateHistory;

        private GameStateHistory()
        {
        }

        public static GameStateHistory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameStateHistory();

                return _instance;
            }
        }

        public void DoGameStateHistoryOperation()
        {
            Console.WriteLine("GameStateHistory operation");
        }

        public GameState GetCurrentState()
        {
            GameState currentState = StateHistory[StateHistory.Count];
            return currentState;
        }

        public void AddHistory(GameState history)
        {
            StateHistory.Add(history);
        }
        
        public GameState GetPrevious()
        {
            GameState previousState = StateHistory[StateHistory.Count - 2];
            return previousState;
        }

        public GameState GetNext()
        {
            GameState nextState = StateHistory[StateHistory.Count + 2];
            return nextState;
        }
    }
}