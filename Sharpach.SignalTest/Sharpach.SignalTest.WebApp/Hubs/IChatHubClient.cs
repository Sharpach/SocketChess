using System.Threading.Tasks;

namespace Sharpach.SignalTest.WebApp.Hubs 
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string msg);
    }
}