namespace Mamba2.Entities
{
    public class EmployeePosition:BaseEntity
    {
        public int PositionId {  get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Position Position { get; set; }
    }
}
