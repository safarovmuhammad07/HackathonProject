using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;

namespace Infrastructure.Interfaces;


public interface IParticipantService
{
    Task<Response<List<ParticipantDTO>>> GetAllParticipantsAsync();
    Task<Response<Participant>> GetParticipantByIdAsync(int id);
    Task<Response<string>> AddParticipantAsync(ParticipantDTO request);
    Task<Response<string>> UpdateParticipantAsync(ParticipantDTO request);
    Task<Response<string>> DeleteParticipantAsync(int id);
}