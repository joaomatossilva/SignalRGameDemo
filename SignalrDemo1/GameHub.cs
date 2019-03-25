using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalrDemo1
{
    public class GameHub : Hub
    {
        // Admin >>> Clients
        public async Task StartRound()
        {
            GameCore.Instance.Reset();
            var color = GameCore.Instance.RoundValue;
            await Clients.All.SendAsync("startRound", color);
        }

        // Clients submit
        public async Task SubmitColor(string name, string value)
        {
            GameCore.Instance.Submit(name, value);
        }

        // Present Results Admin >>> Client
        public async Task ShowResults()
        {
            var results = GameCore.Instance.GetScore();
            await Clients.All.SendAsync("showResults", results);
        }
    }
}
