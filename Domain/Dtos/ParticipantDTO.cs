using Domain.Enums;

namespace Domain.Dtos;

public class ParticipantDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int TeamId { get; set; }
    public ParticipantRole Role { get; set; }
    public DateTime JoinedDate { get; set; }
}