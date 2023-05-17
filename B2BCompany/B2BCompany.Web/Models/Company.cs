using System.ComponentModel.DataAnnotations.Schema;

namespace B2BCompany.Web.Models
{
    public class Company
    {
        public string UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
