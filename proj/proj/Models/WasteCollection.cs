using System.ComponentModel.DataAnnotations;

namespace proj.Models
{
    public class WasteCollection
    {
        //internal readonly object flag;

        public int Collection_id { get; set; }

        public DateTime? EntryDate { get; set; }

        [Required]
        public string Category { get; set; }

        public string Subcategory { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid weight.")]
        public decimal WeightInLbs { get; set; }

        [Required]
        public bool IsRecyclable { get; set; }

        public string flag { get; set; }
    }
}
