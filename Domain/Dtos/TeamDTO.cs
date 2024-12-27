namespace Domain.Dtos;

public class TeamDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HackathonId { get; set; }
    
    public DateTime HackathonDate { get; set; }
}