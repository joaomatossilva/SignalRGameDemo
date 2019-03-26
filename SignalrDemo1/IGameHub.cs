using System.Threading.Tasks;

namespace SignalrDemo1
{
    public interface IGameHub
    {
        Task ShowResults();
        Task StartRound();
        Task SubmitColor(string name, string value);
    }
}