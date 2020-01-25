using System.Collections.Generic;
using System.Linq;
using Kebab.Database.Data;
using Kebab.Database.Models;
using Kebab.Mappers;

namespace Kebab.Database
{
    internal static class DatabaseRepository
    {
        public static readonly KebabDbContext KebabDb = new KebabDbContext();

        public static void AddOrder(Order order)
        {
            KebabDb.OrderDtos.Add(OrderMapper.FromOrder(order));
            KebabDb.SaveChanges();

        }
        public static void AddMetadata(Metadata metadata)
        {

            KebabDb.Metadatas.Add(metadata);
            KebabDb.SaveChanges();
        }

        public static List<Metadata> GetMetadatas()
        {
            return KebabDb.Metadatas.Select(e => e).ToList();
        }

        public static List<Order> GetOrders()
        {
            return KebabDb.OrderDtos.Select(e => OrderMapper.ToOrder(e)).ToList();
        }
    }
}
