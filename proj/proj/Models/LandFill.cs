namespace proj.Models
{
    public class LandFill
    {

        public int landfill_id { get; set; }
        public DateTime? Landfill_Date { get; set; } 

        public int weight_In_Lbs { get; set; }

        public int Expense_In_Usd { get; set; }

        public string LANDFILL_HAULER_NAME { get; set; } 

        public string flag { get; set; } = "";
    }
}
