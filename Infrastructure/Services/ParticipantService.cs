using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ParticipantService(Context context): IParticipantService
{
   public async Task<Response<List<ParticipantDTO>>> GetAllParticipantsAsync()
    {
        var participants =await context.Participants
            .ToListAsync();
        var participantDtos = participants.Select(t => new ParticipantDTO()
        {
            Id = t.Id,
            Name = t.Name,
            Email = t.Email,
            Role = t.Role,
            JoinedDate = t.JoinDate,
            
            
            
        }).ToList();
        return new Response<List<ParticipantDTO>>(participantDtos);
    }

    public async Task<Response<Participant>> GetParticipantByIdAsync(int id)
    {
        var participant= await context.Participants.FirstOrDefaultAsync(t => t.Id == id);
        return participant == null
            ? new Response<Participant>(HttpStatusCode.NotFound, "Participent not found")
            : new Response<Participant>(participant);
    }

    public async Task<Response<string>> AddParticipantAsync(ParticipantDTO request)
    {
        var Participant = new Participant()
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            JoinDate = DateTime.Now,
            
        };
        await context.Participants.AddAsync(Participant);
        var result = await context.SaveChangesAsync();
         return result==0
             ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
             : new Response<string>(HttpStatusCode.Created, "Participant created successfully");
    }

    public async Task<Response<string>> UpdateParticipantAsync(ParticipantDTO request)
    {
        var existingParticipant = await context.Participants.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (existingParticipant == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Participant not found");
        }
        existingParticipant.Id = request.Id;
        existingParticipant.Name = request.Name;
        existingParticipant.Email = request.Email;
        existingParticipant.Role = request.Role;
        existingParticipant.JoinDate = DateTime.Now;
        context.Participants.Update(existingParticipant);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Participant updated successfully");
    }

    public async Task<Response<string>> DeleteParticipantAsync(int id)
    {
        var existingParticipant = await context.Participants.FirstOrDefaultAsync(t => t.Id == id);
        if (existingParticipant == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Participant not found");
        }
        context.Participants.Remove(existingParticipant);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Participant deleted successfully");
    }
}