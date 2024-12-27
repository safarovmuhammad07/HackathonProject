using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;

namespace Infrastructure.Interfaces;

public interface IHackathonService
{
    Task<Response<List<HackathonDTO>>> GetAllHackathonsAsync();
    Task<Response<Hackathon>> GetHackathonByIdAsync(int id);
    Task<Response<string>> AddHackathonAsync(HackathonDTO request);
    Task<Response<string>> UpdateHackathonAsync(HackathonDTO request);
    Task<Response<string>> DeleteHackathonAsync(int id);
}