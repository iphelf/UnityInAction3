namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public interface IGameManager
    {
        ManagerStatus status { get; }
        void StartUp();
    }
}