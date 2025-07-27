namespace USplitAPI.Dtos;

public class TransactionOptionsDto
{
    public int Amount { get; set; }
    public int FamilyId { get; set; }
    public int UserId { get; set; }
    public string SplitType { get; set; } = "equal";
    public string Details { get; set; } = "";
    public List<int> ParticipantList { get; set; } = new();
    public List<TransactionParticipant> ParticipantDetailedList { get; set; } = new();

    public class TransactionParticipant
    {
        public int UserId { get; set; }
        public int Amount { get; set; }
    }
}