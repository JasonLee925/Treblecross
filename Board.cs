using System.Data;

namespace Treblecross
{
    public class Board
    {
        private readonly int dimension_x;
        private readonly int dimension_y;
        private GameState currentState { get; set; }

        public Board(int x, int y) {
            dimension_x = x;
            dimension_y = y;
            // int[,] state = new int[x,y];
            // GameState gameState = new GameState(state);
            // currentState = state;
        }
        public Board(int x, int y, GameState state) {
            dimension_x = x;
            dimension_y = y;
            currentState = state;
        }


        public void Draw () {
            if (currentState != null) {
                // ...
            }
        }

        public void Update(GameState state) {
            currentState = state;
            Draw();
        }

        public void Load(GameState state) {
            Update(state);
        }

    }


}