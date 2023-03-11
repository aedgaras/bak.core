using System.ComponentModel.DataAnnotations.Schema;

namespace bak.models.Models;

public abstract class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}