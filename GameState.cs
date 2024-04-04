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

        public GameState Redo()
        {
            GameState latestState = gameStateHistory.GetLatestState();
            if (latestState.State != State)
            {
                gameStateHistory.Pointer = gameStateHistory.GetNext();
                return gameStateHistory.Pointer;
            }
            else
            {
                gameStateHistory.Pointer = latestState;
                Console.WriteLine("You are on the most updated move.");
            }
            return latestState;
        }

        public GameState Undo()
        {
            gameStateHistory.Pointer = gameStateHistory.GetPrevious();
            return gameStateHistory.Pointer;
        }
    }

    //singleton class
    public class GameStateHistory
    {
        private static GameStateHistory _instance;
        private List<GameState> StateHistory;
        public GameState Pointer;

        private GameStateHistory()
        {
            StateHistory = new List<GameState>();
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

        /// <summary>
        /// For initial calling.
        /// </summary>
        /// <param name="state"></param>
        public void ResetPointer(GameState state) {
            StateHistory.Add(state);
            Pointer = state;
        }

        public GameState GetLatestState()
        {
            GameState currentState = StateHistory[StateHistory.Count -1];
            return currentState;
        }

        public void AddHistory(GameState state)
        {
            int idx = StateHistory.IndexOf(Pointer);
            if (idx == -1) {
                return;
            }

            StateHistory.RemoveRange(idx + 1, StateHistory.Count - idx - 1);
            Pointer = state;
            StateHistory.Add(state);
        }
        
        public GameState GetPrevious()
        {
            GameState previousState = StateHistory[StateHistory.IndexOf(Pointer) - 2]; //-2
            return previousState;
        }

        public GameState GetNext()
        {
            GameState nextState = StateHistory[StateHistory.IndexOf(Pointer) + 2]; // +2
            return nextState;
        }

        public bool Contains(GameState state) { 
            return StateHistory.Contains(state);
        }

    }
}