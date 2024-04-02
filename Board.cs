using System.Data;

namespace Treblecross
{
    public class Board
    {
        private readonly int dimension_x;
        private readonly int dimension_y;
        private GameState currentState;
        public GameState CurrentState { 
                get { return currentState; }  
                set { 
                    currentState = value;
                    Draw(); 
                } 
            }
        private string[,] displayedAry {get; set;}

        public Board(int x, int y) {
            dimension_x = x;
            dimension_y = y;
            GameState gameState = new GameState(new int[x,y]);
            displayedAry = new string[gameState.State.GetLength(0), gameState.State.GetLength(1)];
            CurrentState = gameState;
        }
        public Board(int x, int y, GameState state) {
            dimension_x = x;
            dimension_y = y;
            displayedAry = new string[state.State.GetLength(0), state.State.GetLength(1)];
            CurrentState = state;
        }


        public void Draw () {
            // if (CurrentState == null) {
            //     Console.WriteLine("[Board] Warning: The stored state in board is null, nothing will be drew.");
            //     return;
            // }
            if (currentState.State.GetLength(0) == 1) {
                if (currentState.Player == null) {
                    // init
                    Console.WriteLine("[{0}]", string.Join(", ", Common<string>.Convert1dArray(displayedAry)));
                    return;
                }
                for (int i=0; i < currentState.State.GetLength(1); i++) {
                    if (currentState.State[0, i] != 0) {
                        displayedAry[0, i] = currentState.Player.Piece.Print();
                    }
                }
                Console.WriteLine("[{0}]", string.Join(", ", Common<string>.Convert1dArray(displayedAry)));
            } else {
                // 2D board ..
            }
        }

        public void UpdateAndDraw(GameState state) {
            currentState = state;
            Draw();
        }

        public void Load(GameState state) {
            UpdateAndDraw(state);
        }

    }


}