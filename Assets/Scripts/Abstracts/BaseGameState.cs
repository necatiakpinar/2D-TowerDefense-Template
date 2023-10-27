using NecatiAkpinar.GameStates;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseGameState
    {
        public abstract void Start(GameStateInfoTransporter stateInfoTransporter);
        public abstract void End();
    }
}