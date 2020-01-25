using Kebab.Database.Models;

namespace Kebab.Mappers
{
    internal static class OrderMapper
    {
        public static Order ToOrder(OrderDto orderDto)
        {
            return new Order
            {
                Kind = orderDto.Kind,
                Meal = orderDto.Meal,
                Price = orderDto.Price,
                Sauce = orderDto.Sauce
            };
        }

        public static OrderDto FromOrder(Order order)
        {
            return new OrderDto
            {
                Kind = order.Kind,
                Meal = order.Meal,
                Price = order.Price,
                Sauce = order.Sauce
            };
        }
    }
}
