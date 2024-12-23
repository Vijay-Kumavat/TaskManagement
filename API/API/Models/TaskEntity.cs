namespace API.Models;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}

