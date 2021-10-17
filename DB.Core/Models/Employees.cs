namespace DB.Core.Models
{
    public partial class Employees
    {
        public long EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpFamilyName { get; set; }
        public string EmpFatherName { get; set; }
        public string NationalCode { get; set; }
        public string mobile { get; set; }
        public byte[] image { get; set; }

    }
}
