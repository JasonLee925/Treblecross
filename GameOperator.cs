namespace Treblecross
{
    public enum GameType 
    {
        Treblecross = 0,
        // Reversi = 1,
    }

    public enum GameMode
    {
        PvE = 0,
        PvP = 1,
    }

    public enum GameCommend
    {
        move = 0,
        save = 1,
        hint = 2,
        undo = 3,
        redo = 4,
        quit = 5,
        unknown = -1, // for commend validation
    }

    public delegate bool ValidateMoveCallback(string cmd);

    public abstract class GameOperator
    {
        public abstract string GameId { get; }
        public GameMode Mode { get; }
        protected Player[] players = new Player[2];
        protected Board board;

        public GameOperator()
        {
            int mode = -1;
            bool valid = false;
            do
            {
                Console.WriteLine("[Game] Choose mode? (0: PvE, 1: PvP) ");
                valid = int.TryParse(Console.ReadLine(), out mode);
                if (!Enum.IsDefined(typeof(GameMode), mode) || !valid)
                {
                    Console.WriteLine("Wrong GameMode, please re-enter!");
                    valid = false;
                }
            } while (!valid);
            Mode = (GameMode)mode;
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
            GameStateHistory.Instance.ResetPointer(board.CurrentState);
        }

        protected void init()
        {
            // create players
            if (Mode == GameMode.PvE)
            {
                players.SetValue(Player.CreateHumanPlayer(), 0);
                players.SetValue(Player.CreateComputerPlayer(), 1); // a white 'X'
            }
            else
            {
                // PvP
                players.SetValue(Player.CreateHumanPlayer(), 0);
                Console.WriteLine("[Game] Done, now create another player.");
                players.SetValue(Player.CreateHumanPlayer(), 1);
            }

            // create board
            int size = 10;
            do
            {
                Console.WriteLine("[Game] What is your board size? (Enter a number that is <=5) ");
            } while (!int.TryParse(Console.ReadLine(), out size) || size <= 5);
            board = new Board(1, size);
            GameStateHistory.Instance.ResetPointer(board.CurrentState);
            board.Draw();
        }

        public void Start()
        {
            // looping players
            int idx = 0;
            bool next = true;
            while (next)
            {
                Console.WriteLine("[Game] {0}'s turn!", players[idx].Name);
                makeMove(players[idx], ref next);
                if (idx >= players.Length - 1)
                {
                    idx = 0;
                }
                else
                {
                    idx++;
                }
            }
        }

        /// <summary>
        /// Impletement template pattern.
        /// Main logic of making a move.
        /// </summary>
        /// <param name="player"></param>
        private void makeMove(Player player, ref bool next)
        {
            // 1. make move
            GameState state;
            if (player.PlayerType == PlayerType.Cpu)
            {
                state = makeRandomMove(player);
            }
            else
            {
                // human player
                state = makeMove(player);
            }

            GameStateHistory.Instance.AddHistory(state);

            // 2. update board
            updateBoard(state);

            // 3. determine winner
            if (determineWinner(state))
            {
                Player winner = state.Player;
                if (winner.PlayerType == PlayerType.Cpu) {
                    Console.WriteLine("[Game] The winner is CPU. Speechless.");
                } else {
                    Console.WriteLine("[Game] The winner is {0}. Congrats!", state.Player.Name);
                }
                next = false;
                return;
            }

            next = true;
        }

        protected static GameCommend validateGameCommend(string cmd, ValidateMoveCallback vMoveCallabck)
        {
            if (vMoveCallabck(cmd))
            {
                return GameCommend.move;
            } 

            GameCommend gcmd;
            if (!int.TryParse(cmd, out _) & Enum.TryParse<GameCommend>(cmd, out gcmd)) // if it's not a number and is one of the GameCommends
            {
                return gcmd;
            }

            return GameCommend.unknown; // represents invalid 
        }

        protected abstract GameState makeRandomMove(Player player);
        protected abstract GameState makeMove(Player player);
        protected abstract bool validateMove(string move);
        protected abstract void updateBoard(GameState state);
        protected abstract bool determineWinner(GameState state);
        protected abstract void end();
        protected abstract void getHint();
    }

    public class TreblecrossOperator : GameOperator
    {
        public override string GameId => "Treblecross";

        public TreblecrossOperator()
        {
            Console.WriteLine("[Game] Creating Treblecross ... ");
            base.init();
        }

        public TreblecrossOperator(GameMode mode, Player[] players, Board board) : base(mode, players, board) { 
            Console.WriteLine("[Game] Creating Treblecross ... ");
            this.board.Draw();
        }


        protected override GameState makeRandomMove(Player player)
        {
            int[,] state = (int[,])board.CurrentState.State.Clone();
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[0, j] == 0) {
                    state[0, j] = player.Id; // update
                    break;
                }
            }
            Thread.Sleep(1000);
            return new GameState(player, state);
        }

        protected override GameState makeMove(Player player)
        {
            int move = -1;
            Console.Write("[Game] Enter a game command or a number from 1 ~ {0}: ", board.CurrentState.State.GetLength(1));
            string cmd = Console.ReadLine();
            GameCommend gcmd = validateGameCommend(cmd, validateMove);

            switch (gcmd)
            {
                case GameCommend.move:
                    move = int.Parse(cmd);
                    move -= 1 ;
                    int[,] state = (int[,])board.CurrentState.State.Clone();
                    state[0, move] = player.Id; // update
                    return new GameState(player, state);
                case GameCommend.undo:
                    board.Update(board.CurrentState.Undo());
                    GameState newMove = makeMove(player);
                    return newMove;
                case GameCommend.redo:
                    board.Update(board.CurrentState.Redo());
                    return makeMove(player);
                case GameCommend.hint:
                    getHint();
                    return makeMove(player);
                case GameCommend.save:
                    saveGame();
                    return makeMove(player);
                case GameCommend.quit:
                    end();
                    break;
                default:
                    Console.WriteLine("[Game] Invalid command/move.");
                    return makeMove(player);
            }
            return null;
        }

        private void saveGame() {
            using (GameFile file = new GameFile())
            {
                GameData gamedata = new GameData();
                BoardInfo boardinfo = new BoardInfo();
                boardinfo.State = Common<int>.Convert2DArrayTo2DList(board.CurrentState.State);
                boardinfo.Player = board.CurrentState.Player.Id;
                boardinfo.Size = board.Size;
                gamedata.Board = boardinfo;

                PieceInfo[] pieceInfos = new PieceInfo[players.Length];
                for (int i = 0; i < pieceInfos.Length; i++)
                {
                    Player p = players[i];
                    PieceInfo pieceInfo = new PieceInfo();
                    pieceInfo.Id = p.Piece.Id;
                    pieceInfo.Mark = p.Piece.Mark;
                    pieceInfo.Colour = p.Piece.Colour;
                    pieceInfos[i] = pieceInfo;
                }

                PlayerInfo[] playerInfos = new PlayerInfo[players.Length];
                for (int i = 0; i < playerInfos.Length; i++)
                {
                    Player p = players[i];
                    PlayerInfo playerInfo = new PlayerInfo();
                    playerInfo.Id = p.Id;
                    playerInfo.Name = p.Name;
                    playerInfo.Type = p.PlayerType;
                    playerInfo.Piece = pieceInfos[i];
                    playerInfos[i] = playerInfo;
                }

                gamedata.Players = playerInfos.ToList();

                GameInfo gameInfo = new GameInfo();
                gameInfo.Name = GameId;
                gameInfo.Mode = Mode;
                gamedata.Game = gameInfo;

                file.Save(gamedata);
            }
        }


        protected override void updateBoard(GameState state)
        {
            board.Update(state);
        }

        protected override bool validateMove(string move)
        {
            int moveInt;
            if (!int.TryParse(move, out moveInt)) return false;

            int[,] stateAry = board.CurrentState.State;
            if (moveInt > stateAry.GetLength(1)) return false;
            moveInt -= 1;
            if (stateAry[0, moveInt] != 0) return false;

            return true;
        }
            
        protected override void getHint()
        {
            int[] firstRow = Common<int>.Convert1dArray(board.CurrentState.State);
            int[] available_positions = getZeroIndexes(firstRow).Select(x => x+1).ToArray();
            string gameCommand = "Game commends: \r\n" + "undo: Undo a move (Go back to a previous move).\r\n" +
                        "redo: Redo a move (Available when the board is not at the latest state).";
            Console.WriteLine("[Game] Hint\r\n-\r\nHere are some available positions: {0}\r\n-\r\n{1}\r\n{2}",
                                            string.Join(", ",available_positions), gameCommand, ProgramEngine.GlobalCommends);
        }

        private int[] getZeroIndexes(int[] array)
        {
            List<int> ret = new List<int>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    ret.Add(i);
                }
            }

            return ret.ToArray();
        }

        protected override bool determineWinner(GameState state)
        {
            int[] firstRow = Common<int>.Convert1dArray(state.State);
            return hasThreeNonZeroInARow(firstRow);
        }

        private static bool hasThreeNonZeroInARow(int[] array)
        {
            int count = 0;
            foreach (int num in array)
            {
                if (num != 0)
                {
                    count++;
                    if (count == 3)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            return false;
        }

        protected override void end()
        {
            Console.WriteLine("Bye!");
            Environment.Exit(1);
        }

    }


    // public class ReversiOperator : GameOperator {}
}