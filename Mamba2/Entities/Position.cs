namespace Mamba2.Entities
{
    public class Position:BaseEntity
    {
        public string Name {  get; set; }
        
        public List<EmployeePosition> EmployeePositions { get; set; }
    }
}
