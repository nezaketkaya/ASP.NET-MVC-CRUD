namespace AspNetMvcCrud.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
