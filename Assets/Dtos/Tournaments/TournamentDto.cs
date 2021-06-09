using System;
using Enum;

namespace Dtos.Tournaments
{
    [Serializable]
    public class TournamentDto
    {
        public string Name;
        public string Id;
        public int Code;
        public TournamentStatus tournamentStatus;
        public int WinnerUserGroupId;
    }
}