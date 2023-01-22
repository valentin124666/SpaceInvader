using Cysharp.Threading.Tasks;
using Settings;

namespace Managers.Interfaces
{
    public interface IGameplayManager 
    {
        T GetController<T>() where T : IController;

        Enumerators.AppState CurrentState { get; }
        bool IsPause { get;}
        bool EndGame { get;}
        void EnablePause();
        UniTask StartGameplay();
        void StopGameplay();
        void RefreshGameplay();
        void ChangeAppState(Enumerators.AppState stateTo);

    }
}
