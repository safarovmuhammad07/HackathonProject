using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TeamService(Context context): ITeamService
{
    public async Task<Response<List<TeamDTO>>> GetAllTeamsAsync()
    {
        var teams =await context.Teams
            .ToListAsync();
        var TeamDtos = teams.Select(t => new TeamDTO()
        {
            Name = t.Name,
            HackathonId = t.HackathonId,
            HackathonDate = t.HackathonDate,
        }).ToList();
        return new Response<List<TeamDTO>>(TeamDtos.ToList());
    }

    public async Task<Response<Team>> GetTeamByIdAsync(int id)
    {
        var team= await context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        return team == null
            ? new Response<Team>(HttpStatusCode.NotFound, "Course not found")
            : new Response<Team>(team);
    }

    public async Task<Response<string>> AddTeamAsync(TeamDTO request)
    {
        var team = new Team()
        {
            Name = request.Name,
            HackathonId = request.HackathonId,
            HackathonDate = request.HackathonDate,
        };
        await context.Teams.AddAsync(team);
        var result = await context.SaveChangesAsync();
         return result==0
             ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
             : new Response<string>(HttpStatusCode.Created, "Team created successfully");
    }

    public async Task<Response<string>> UpdateTeamAsync(TeamDTO request)
    {
        var existingTeam = await context.Teams.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (existingTeam == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Team not found");
        }
        existingTeam.Name = request.Name;
        existingTeam.HackathonId = request.HackathonId;
        existingTeam.HackathonDate = request.HackathonDate;
        context.Teams.Update(existingTeam);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Team updated successfully");
    }

    public async Task<Response<string>> DeleteTeamAsync(int id)
    {
        var existingTeam = await context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTeam == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Team not found");
        }
        context.Teams.Remove(existingTeam);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Team deleted successfully");
    }
}