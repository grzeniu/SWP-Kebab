namespace Kebab
{
    public class Order
    {
        public string Meal { get; set; } = "";
        public string Kind { get; set; } = "";
        public string Sauce { get; set; } = "";
        public string Price { get; set; }

        public void ResetOrder()
        {
            Meal = "";
            Kind = "";
            Sauce = "";
            Price = "";
        }

        public bool OrderReady()
        {
            return !string.IsNullOrEmpty(Meal) && !string.IsNullOrEmpty(Kind) && !string.IsNullOrEmpty(Sauce);
        }
    }
}
