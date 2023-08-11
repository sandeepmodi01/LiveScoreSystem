using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScore.Models
{
    [Table("Match")]
    public class MatchModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("TeamOne")]
        public int TeamOneId { get; set; }

        public TeamModel TeamOne { get; set; }

        [ForeignKey("TeamTwo")]
        public int TeamTwoId { get; set; }
        public TeamModel TeamTwo { get; set; }

        [ForeignKey("WinnerTeam")]
        public int? WinnerTeamId { get; set; }

        public TeamModel WinnerTeam { get; set; }
        
        [ForeignKey("LoosingTeam")]
        public int? LoosingTeamId { get; set; }

        public TeamModel LoosingTeam { get; set; }

        public int FirstTeamTotalRun { get; set; }

        public int FirstTeamTotalWicket { get; set; }

        public int SecondTeamTotalRun { get; set; }

        public int SecondTeamTotalWicket { get; set; }

        public IList<ScoreCardModel> ScoreCard { get; set; }

    }
}
