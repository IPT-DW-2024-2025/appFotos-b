using appFotos.Data;
using appFotos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

    [Authorize]
    public void AtualizarGostos(int idFoto)
    {
        // 1 - verificar se a foto existe
        var foto= _applicationDbContext.Fotografias.Find(idFoto);
        if (foto == null)
            return;
        
        // 2 - validar se o utilizador já gostou da foto 
        var utilizador = _applicationDbContext.Utilizadores
            .Include(u=> u.ListaGostos)
            .First(u => u.IdentityUserName == Context.User.Identity.Name);

        var listaGostoFoto = utilizador.ListaGostos
            .Where(ul => ul.FotografiaFk == idFoto);

        // se entra aqui é porque existe um gosto
        if (listaGostoFoto.Any())
        {
            _applicationDbContext.Gostos.Remove(listaGostoFoto.First());
        }
        // se entra aqui é porque não existe um gosto
        else
        {
            var novoGosto = new Gostos(){FotografiaFk = idFoto, Data = DateTime.Now, UtilizadorFk = utilizador.Id};
            _applicationDbContext.Gostos.Add(novoGosto);
        }
    
        _applicationDbContext.SaveChanges();
        
        var numGostos = _applicationDbContext.Gostos.Where(g => g.FotografiaFk==idFoto).Count();
        
        Clients.All.SendAsync("AtualizarGostos", idFoto, numGostos);

    }
    
}