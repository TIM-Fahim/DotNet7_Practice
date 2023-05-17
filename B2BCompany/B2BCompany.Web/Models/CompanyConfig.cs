using System.ComponentModel.DataAnnotations.Schema;

namespace B2BCompany.Web.Models
{
    public class CompanyConfig
    {
        public string UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyConfigID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } = "Text";
    }
}
