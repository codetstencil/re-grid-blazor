using BlazorApp9.Data;
using BlazorApp9.Models;
using GridCore.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace BlazorApp9.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly DbContextOptions<NorthwindDbContext> _options;

        public OrderDetailService(DbContextOptions<NorthwindDbContext> options)
        {
            _options = options;
        }

        public async Task<ItemsDTO<OrderDetail>> GetOrderDetailsGridRowsAsync(Action<IGridColumnCollection<OrderDetail>> columns,
            object[] keys, QueryDictionary<StringValues> query)
        {
            using (var context = new NorthwindDbContext(_options))
            {
                int orderId;
                int.TryParse(keys[0].ToString(), out orderId);
                var repository = new OrderDetailsRepository(context);
                var server = new GridCoreServer<OrderDetail>(repository.GetForOrder(orderId), query, true, "orderDetailssGrid" + keys[0].ToString(), columns)
                        .Sortable()
                        .WithPaging(10)
                        .Filterable()
                        .WithMultipleFilters()
                        .SetRemoveDiacritics<NorthwindDbContext>("RemoveDiacritics");

                // return items to displays
                var items = await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());
                return items;
            }
        }

        public async Task<OrderDetail> Get(params object[] keys)
        {
            using (var context = new NorthwindDbContext(_options))
            {
                int orderId;
                int productId;
                int.TryParse(keys[0].ToString(), out orderId);
                int.TryParse(keys[1].ToString(), out productId);
                var repository = new OrderDetailsRepository(context);
                return await repository.GetById(new { OrderID = orderId, ProductID = productId });
            }
        }

        public async Task Insert(OrderDetail item)
        {
            using (var context = new NorthwindDbContext(_options))
            {
                try
                {
                    var repository = new OrderDetailsRepository(context);
                    await repository.Insert(item);
                    repository.Save();
                }
                catch (Exception e)
                {
                    throw new GridException("DETSRV-01", e);
                }
            }
        }

        public async Task Update(OrderDetail item)
        {
            using (var context = new NorthwindDbContext(_options))
            {
                try
                {
                    var repository = new OrderDetailsRepository(context);
                    await repository.Update(item);
                    repository.Save();
                }
                catch (Exception e)
                {
                    throw new GridException(e);
                }
            }
        }

        public async Task Delete(params object[] keys)
        {
            using (var context = new NorthwindDbContext(_options))
            {
                try
                {
                    var order = await Get(keys);
                    var repository = new OrderDetailsRepository(context);
                    repository.Delete(order);
                    repository.Save();
                }
                catch (Exception)
                {
                    throw new GridException("Error deleting the order detail");
                }
            }
        }
    }

    public interface IOrderDetailService : ICrudDataService<OrderDetail>
    {
        Task<ItemsDTO<OrderDetail>> GetOrderDetailsGridRowsAsync(Action<IGridColumnCollection<OrderDetail>> columns,
            object[] keys, QueryDictionary<StringValues> query);
    }
}
