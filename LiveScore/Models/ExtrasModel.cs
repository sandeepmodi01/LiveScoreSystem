using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models
{
    [Table("Extras")]
    public class ExtrasModel
    {
        public int Id { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public MatchModel Match { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public TeamModel Team { get; set; }
        public int Run { get; set; }
    }
}
