using System.Collections;

namespace CuidandoPawsApi.Domain.Models;

public class ServiceCatalog
{
    public int Id { get; set; }
    
    public string? NameService { get; set; }
    
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;    
    
    public string? Type { get; set; }

    public bool IsAvaible { get; set; }
    public int Duration { get; set; }
    
    public ICollection<Appoinment>? Appoinment { get; set; }
    
}