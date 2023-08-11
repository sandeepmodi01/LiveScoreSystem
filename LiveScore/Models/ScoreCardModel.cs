using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models
{
    [Table("ScoreCard")]
    public class ScoreCardModel
    {
        public int Id { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public MatchModel Match { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public PlayerModel Player { get; set; }

        public int Run { get; set; }

        public int Wicket { get; set; }
    }
}
