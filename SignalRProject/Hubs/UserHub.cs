using Microsoft.AspNetCore.SignalR;

namespace SignalRProject.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews {  get; set; }
        public static int TotalUsers {  get; set; }

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("UpdateTotalUsers", TotalUsers);
            return base.OnDisconnectedAsync(exception);
        }


        public async Task NewWindowLoaded()
        {
            TotalViews++;

            // rpc call , call all the clients that totalviews has updated
            // clients.All comes from Hub
            // in brancket we pass client side fucntion and pass parameter
            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
        }
    }
}
