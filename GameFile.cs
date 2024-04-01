namespace Treblecross 
{
    public class GameFile : IDisposable
    {
        public void Dispose()
        {
            // Dispose(true);
            GC.SuppressFinalize(this);
        }


        public (GameMode, Player[], Board) Load() {
            // implement the objects
            return (GameMode.None, null, null);
        }
    }

}