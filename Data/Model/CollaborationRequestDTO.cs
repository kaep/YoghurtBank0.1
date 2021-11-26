
namespace YoghurtBank.Data.Model
{ 
    public record CollaborationRequestCreateDTO
    {
        public int StudentId { get; set;}
        public int SupervisorId { get; set;}
        public int? IdeaId { get; set; }
        public string Application { get; set; } 
    }

    public record CollaborationRequestUpdateDTO
    {
        public int Id { get; set; }
        public CollaborationRequestStatus Status { get; set; }
    }

    public record CollaborationRequestDetailsDTO 
    {
        public int StudentId {get; set;}
        public int Supervisor {get; set;}
        public string Application { get; set; } 
        public CollaborationRequestStatus Status { get; set; }
    }
}
