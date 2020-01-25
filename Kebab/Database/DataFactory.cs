using Kebab.Database.Models;

namespace Kebab.Database
{
    public static class DataFactory
    {
        public static void CreateMetadata()
        {
            DatabaseRepository.ClearMetadata();
            // To be implemented specific metadata
            DatabaseRepository.AddMetadata(new Metadata { Label = "XML", Value = "true" });

        }
        public static void CreateSampleOrders()
        {
            var o1 = new Order
            {
                Meal = "kebaba",
                Kind = "w bułce",
                Sauce = "ostry",
                Price = "12.22"
            };

            var o2 = new Order
            {
                Meal = "frytki",
                Kind = "belgijskie",
                Sauce = "łagodny",
                Price = "9.50"

            };

            DatabaseRepository.AddOrder(o1);
            DatabaseRepository.AddOrder(o2);
        }
    }
}
