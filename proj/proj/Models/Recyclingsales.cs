using System.ComponentModel.DataAnnotations;

namespace proj.Models
{
    public class Recyclingsales

    {

        public int Revenue_id { get; set; }


      
        public DateTime? Sales_date { get; set; }

       
        public  string Material_Category { get; set; }

        public  string Material_SubCategory { get; set; }

        [Display(Name = "Material Weight (lbs)")]
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Material_Weight_lbs { get; set; }

  
        [Range(0.01, double.MaxValue, ErrorMessage = "Revenue must be greater than 0.")]
        public decimal Revenue_in_USD { get; set; }

        
        public string Buyer_Name { get; set; }

        public string flag {  get; set; }

     
    }
}
