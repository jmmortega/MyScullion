namespace MyScullion.Models
{
    public class Measure
    {
        public int IngredientId { get; set; }

        public int MeasureId { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public double Grams { get; set; }

        public override string ToString() => $"{Amount} {Description} {Grams}";

        public string ToCSV()
        {
            return $"{IngredientId};{MeasureId};{Description.Trim()};{Amount};{Grams}";
        }
        
    }
}
