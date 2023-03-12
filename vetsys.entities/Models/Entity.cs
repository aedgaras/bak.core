using System.ComponentModel.DataAnnotations.Schema;

namespace vetsys.entities.Models;

public abstract class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}