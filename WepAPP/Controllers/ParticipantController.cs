using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WepAPP.Controllers;
[ApiController]
[Route("[controller]")]
public class ParticipantController(IParticipantService service)
{
    [HttpGet] public async Task<Response<List<ParticipantDTO>>> GetParticiants() => await service.GetAllParticipantsAsync();

    [HttpGet("{id:int}")]
    public async Task<Response<Participant>> GetParticiant(int id) => await service.GetParticipantByIdAsync(id);
    
    [HttpPost] public async Task<Response<string>> AddParticiant(ParticipantDTO particiant)   => await service.AddParticipantAsync(particiant);
    
    [HttpPut] public async Task<Response<string>> UpdateParticiant(ParticipantDTO particiant)  => await service.UpdateParticipantAsync(particiant);
    
    [HttpDelete]  public async Task<Response<string>> DeleteParticiant(int id) => await service.DeleteParticipantAsync(id);
}