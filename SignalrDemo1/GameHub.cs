namespace SignalrDemo1
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class GameHub : Hub, IGameHub
    {
        public async Task StartRound()
        {
            GameCore.Instance.Reset();
            var color = GameCore.Instance.RoundValue;
            await Clients.All.SendAsync("startRound", color);
        }

        public Task SubmitColor(string name, string value)
        {
            GameCore.Instance.Submit(name, value);
            return Task.FromResult(0);
        }

        public async Task ShowResults()
        {
            var results = GameCore.Instance.GetScore();
            await Clients.All.SendAsync("showResults", results);
        }
    }
}