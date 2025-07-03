namespace L3WebProjet.Common.DTO
{
    public class LeaderboardDto
    {
        public Guid UserId { get; set; }
        public string Pseudo { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}