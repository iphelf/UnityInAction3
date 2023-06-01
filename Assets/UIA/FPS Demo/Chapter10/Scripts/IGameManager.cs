using UIA.TPS_Demo.Chapter09.Scripts;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public interface IGameManager
    {
        ManagerStatus status { get; }
        void StartUp(NetworkService service);
    }
}