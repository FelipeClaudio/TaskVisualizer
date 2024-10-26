using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaskVisualizerWeb.Domain.Models.Commons;

public class Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember]
    [Column("id", Order = 0)]
    public int Id { get; set; }
}
