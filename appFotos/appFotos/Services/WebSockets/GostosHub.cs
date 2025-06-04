using appFotos.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace appFotos.Services.WebSockets;

public class GostosHub : Hub
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILogger<GostosHub> _logger;

    public GostosHub(ILogger<GostosHub> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    
    
    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Gostos connected: "+Context.ConnectionId);
        
        Clients.All.SendAsync("OnConnectedAsync", "Temos uma nova conexão: "+Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }
    
}