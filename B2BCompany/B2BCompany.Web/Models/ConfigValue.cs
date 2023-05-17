using System.ComponentModel.DataAnnotations.Schema;

namespace B2BCompany.Web.Models
{
    public class ConfigValue
    {
        public int ComapanyConfigID { get; set; }

        public int ComapnyId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigValueID { get; set; }
        public string Name { get; set; }
        public string Values { get; set; }
    }
   
}
