using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.ApiSolution;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class HackathonService(Context context): IHackathonService
{
    public async Task<Response<List<HackathonDTO>>> GetAllHackathonsAsync()
    {
        var hackathons =await context.Hackathons
            .ToListAsync();
        var hackathonDtos = hackathons.Select(t => new HackathonDTO()
        {
            Id = t.Id,
            Name = t.Name,
            Date = t.Date,
            Theme = t.Theme
        }).ToList();
        return new Response<List<HackathonDTO>>(hackathonDtos.ToList());
    }

    public async Task<Response<Hackathon>> GetHackathonByIdAsync(int id)
    {
        var hackathon= await context.Hackathons.FirstOrDefaultAsync(t => t.Id == id);
        return hackathon == null
            ? new Response<Hackathon>(HttpStatusCode.NotFound, "Course not found")
            : new Response<Hackathon>(hackathon);
    }

    public async Task<Response<string>> AddHackathonAsync(HackathonDTO request)
    {
        var hackathon = new Hackathon()
        {
            Id = request.Id,
            Name = request.Name,
            Date = request.Date,
            Theme = request.Theme
        };
        await context.Hackathons.AddAsync(hackathon);
        var result = await context.SaveChangesAsync();
         return result==0
             ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
             : new Response<string>(HttpStatusCode.Created, "Hackathon created successfully");
    }

    public async Task<Response<string>> UpdateHackathonAsync(HackathonDTO request)
    {
        var existingHackathon = await context.Hackathons.FirstOrDefaultAsync(t => t.Id == request.Id);
        if (existingHackathon == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Hackathon not found");
        }
        existingHackathon.Id = request.Id;
        existingHackathon.Name = request.Name;
        existingHackathon.Date = request.Date;
        existingHackathon.Theme = request.Theme;
        context.Hackathons.Update(existingHackathon);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Hackathon updated successfully");
    }

    public async Task<Response<string>> DeleteHackathonAsync(int id)
    {
        var existingHackathon = await context.Hackathons.FirstOrDefaultAsync(t => t.Id == id);
        if (existingHackathon == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Hackathon not found");
        }
        context.Hackathons.Remove(existingHackathon);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Response<string>(HttpStatusCode.OK, "Hackathon deleted successfully");
    }
}