using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models
{
    [Table("Player")]
    public class PlayerModel
    {
        public int Id { get; set; }

        [DisplayName("Player Name")]
        public string Name { get; set; }

        public bool IsBatsman { get; set; }

        [DisplayName("Batting Style")]
        public string BattingStyle { get; set; }

        [DisplayName("Bowling Style")]
        public string BowlingStyle { get; set; }

        [DisplayName("Player Image")]
        public string FileName { get; set; }

        [ForeignKey("Team")]

        [DisplayName("Team Name")]
        public int TeamId { get; set; }

        public TeamModel Team { get; set; }

    }
}
