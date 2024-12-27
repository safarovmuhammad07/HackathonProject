using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WepAPP.Controllers;
[ApiController]
[Route("[controller]")]
public class HackathonController(IHackathonService service)
{
    [HttpGet("GetHackatons")]  public async Task<Response<List<HackathonDTO>>> GetHackatons()    => await service.GetAllHackathonsAsync();
    
    [HttpPost("AddHackaton")]  public async Task<Response<string>> AddHackaton(HackathonDTO hackaton)     => await service.AddHackathonAsync(hackaton);
    
    [HttpPut("UpdateHackaton")]   public async Task<Response<string>> UpdateHackaton(HackathonDTO hackaton)=> await service.UpdateHackathonAsync(hackaton);

    [HttpDelete("DeleteHackaton")] public async Task<Response<string>> DeleteHackaton(int id) => await service.DeleteHackathonAsync(id);

    [HttpGet("{id:int}")]  public async Task<Response<Hackathon>> GetHackathonTeamById(int id)  => await service.GetHackathonByIdAsync(id);
}