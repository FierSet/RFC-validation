using System.ComponentModel.DataAnnotations;

namespace RFC.Models;

public class EmployeeModel
{
    public int ID { get; set;}
    
    [Required]
    public string Name { get; set;}

    [Required]
    public string LastName { get; set;}
    
    [Required]
    [RegularExpression(@"^([A-Z]{3}[0-9]{6}[A-Z]{3}[A-Z0-9]{3})$", ErrorMessage = "Invalid RFC format.")]
    public string RFC { get; set;}

    [Required]
    public DateTime BornDate { get; set; }

    [Required]
    public EmployeeStatus Status {get; set;}
    
}

public enum EmployeeStatus
{
    NotSet,
    Active,
    Inactive,
}