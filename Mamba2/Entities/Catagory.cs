namespace Mamba2.Entities
{
    public class Catagory:BaseEntity
    {
        public string Name {  get; set; }
        public List<Employee> Employees { get; set; }
    }
}
