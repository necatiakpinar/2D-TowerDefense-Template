namespace NecatiAkpinar.GameStates
{
    public class GameStateInfoTransporter
    {
        private bool _isLevelWin;

        public bool IsLevelWin => _isLevelWin;

        public GameStateInfoTransporter()
        {
            
        }

        public GameStateInfoTransporter(bool isLevelWin)
        {
            _isLevelWin = isLevelWin;
        }
    }
}