using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;

namespace Infrastructure.Interfaces;
public interface ITeamService

{
    Task<Response<List<TeamDTO>>> GetAllTeamsAsync();
    Task<Response<Team>> GetTeamByIdAsync(int id);
    Task<Response<string>> AddTeamAsync(TeamDTO request);
    Task<Response<string>> UpdateTeamAsync(TeamDTO request);
    Task<Response<string>> DeleteTeamAsync(int id);
}