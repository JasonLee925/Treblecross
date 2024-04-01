
namespace Treblecross
{
    //     public delegate GameOperator StartNewGameDelegate();
    //     public delegate void LoadGameDelegate();

    public delegate GameOperator MenuCallback();

    public enum MenuAction
    {
        StartNewGame = 0,
        LoadGame = 1,
        AvailableCommends = 2,
        Quit = 3,
    }

    public enum GlobalCommend
    {
        save,
        hint,
        undo,
        redo,
        quit,
    }

    public class ProgramEngine
    {
        public Delegate DisplayMenu()
        {
            int action = -1;
            bool valid = false;

            do
            {
                Console.WriteLine("[Menu] Choose an action? (0: Start a new game, 1: Load game, 2: Quit!)");
                valid = int.TryParse(Console.ReadLine(), out action);
                if (!Enum.IsDefined(typeof(MenuAction), action) || !valid)
                {
                    Console.WriteLine("Wrong action, please re-enter!");
                    valid = false;
                }
            } while (!valid);

            switch ((MenuAction)action)
            {
                case MenuAction.StartNewGame:
                    return new MenuCallback(startNewGame);
                case MenuAction.LoadGame:
                    return new MenuCallback(loadGame);
                case MenuAction.AvailableCommends:
                    // prompt available commends
                    Console.WriteLine("[Menu] In the game, there are some global commands that are available to use:");
                    Console.WriteLine(
                        "save: Save game, the command will save the game of current state to a file which can be loaded from the menu.\r\n" +
                        "hint: This will give a player a hint of what positions on the board are available to make a move. It also reminds user of what commands are available.\r\n" +
                        "undo: Undo a move (Go back to a previous move).\r\n" +
                        "redo: Redo a move (Available when the board is not at the latest state).\r\n" +
                        "quit: Quit the game without saving."
                    );
                    return DisplayMenu();
                default:
                    return null;
            }
        }

        public GameOperator startNewGame()
        {
            // TODO: prompt "choose game"   
            return new TreblecrossOperator();
        }

        public GameOperator loadGame()
        {
            GameOperator gameOp;
            using (GameFile file = new GameFile()){
                var reuslt = file.Load();
                GameMode gameMode = reuslt.Item1;
                Player[] players = reuslt.Item2;
                Board board = reuslt.Item3;
                // TODO: Load game type  
                gameOp = new TreblecrossOperator(gameMode, players, board);
            }
            return gameOp;
        }


        public static bool ValidateGlobalCommend(string cmd)
        {
            // might also have to accept argument for custom rule (maybe a regexr)
            return Enum.TryParse<GlobalCommend>(cmd, out _);
        }

    }
}