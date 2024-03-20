
namespace Treblecross
{
    public delegate GameOperator StartNewGameDelegate();
    public delegate void LoadGameDelegate();


    public class ProgramEngine
    {
        // GameOperator game_op;
        // Board board;
        // Command cmd;

        // public ProgramEngine(Command command) {
        //     cmd = command;
            
        // }

        public Delegate DisplayMenu(){
            // 1. start new game
            // 2. load game 
            // 3. end (quit the program)

            // return callback function
            return new StartNewGameDelegate(startNewGame); // test
        }

        public GameOperator startNewGame() {
            // prompt "choose game" -> "choose Mode"
            // Create game operator
            // game_op.Init()
            Console.WriteLine("heyyy");
            return null; // return gameOperator
        }

        public void loadGame() {
            // prompt "enter file path"
            // create file object and call load file
            // create game operator and load information
            // create board and load board

        }


        public static bool ValidateCommend() {
            return false;
        } 

    }

}