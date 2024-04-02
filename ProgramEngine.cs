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

    public class ProgramEngine
    {
        public static string GlobalCommends = "save: Save game, the command will save the game of current state to a file which can be loaded from the menu.\r\n" +
                        "hint: Return a hint about the game. It also reminds user of what commands are available.\r\n" +
                        "quit: Quit the game without saving.";

        public Delegate DisplayMenu()
        {
            int action = -1;
            bool valid = false;

            do
            {
                Console.WriteLine("[Menu] Choose an action? (0: Start a new game, 1: Load game, 2: Check global commands, 3: Quit!)");
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
                    Console.WriteLine(GlobalCommends);
                    return DisplayMenu();
                default:
                    Console.WriteLine("Bye!");
                    return null;
            }
        }

        public GameOperator startNewGame()
        {
            // (not urgent) TODO: prompt "choose game"   
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
                // (not urgent) TODO: Load game type  
                gameOp = new TreblecrossOperator(gameMode, players, board);
            }
            return gameOp;
        }
    }
}