using System.Threading.Tasks;

namespace SignalrDemo1
{
    public interface IGameHub
    {
        Task StartRound();
        Task SubmitColor(string name, string value);
        Task ShowResults();
    }
}