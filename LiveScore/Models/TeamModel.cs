using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models
{
    [Table("Team")]
    public class TeamModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Coach Name")]
        public string CoachName { get; set; }

        [DisplayName("Team Flag")]
        public string FileName { get; set; }

    }
}
