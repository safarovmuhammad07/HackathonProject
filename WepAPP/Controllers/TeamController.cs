using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WepAPP.Controllers;
[ApiController]
[Route("[controller]")]
public class TeamController(ITeamService teamService) 
{
    [HttpGet("GetTeams")]
    public async Task<Response<List<TeamDTO>>> GetTeams() => await teamService.GetAllTeamsAsync();

    [HttpGet("GetTeam/{id}")]
    public async Task<Response<Team>> GetTeam(int id) => await teamService.GetTeamByIdAsync(id);
    
    [HttpDelete("DeleteTeam")]
    public async Task<Response<string>> DeleteTeam(int id) => await teamService.DeleteTeamAsync(id);
    
    [HttpPost("AddTeam")]
    public async Task<Response<string>> AddTeam(TeamDTO team) => await teamService.AddTeamAsync(team);
    
    [HttpPut("UpdateTeam")]
    public async Task<Response<string>> UpdateTeam(TeamDTO team) => await teamService.UpdateTeamAsync(team);
 
}