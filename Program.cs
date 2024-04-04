namespace Treblecross
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(ProgramEngine.ValidateGameCommend("save"));
            // Console.WriteLine(ProgramEngine.ValidateGameCommend("asdfasd"));

            ProgramEngine pe = new ProgramEngine();
            Delegate fn = pe.DisplayMenu();
            if (fn == null) return;
            var opt = fn.DynamicInvoke() ?? new TreblecrossOperator();
            GameOperator gameOp =  (GameOperator) opt;
            gameOp.Start();

            // GameOperator gameOp = startNewGame();

        }
    }
}
