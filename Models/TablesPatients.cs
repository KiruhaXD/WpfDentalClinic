using System.ComponentModel.DataAnnotations.Schema;


namespace WpfAppCallingApi.Models
{
    [Table("Patients")]
    public class Patients
    {
        [Column("ID_Patient")]
        public int Id { get; set; }

        [Column("Full_name")]
        public string fullName { get; set; }

        [Column("Data_birth")]
        public string dateBirth { get; set; }

        [Column("Phone_number")]
        public string phoneNumber { get; set; }

        [Column("Email")]
        public string email { get; set; }

        [Column("Address")]
        public string address { get; set; }
    }
}
