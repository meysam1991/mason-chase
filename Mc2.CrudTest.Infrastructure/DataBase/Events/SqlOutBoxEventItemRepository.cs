using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Mc2.CrudTest.Shared.Configuration;
using Dapper;

namespace Mc2.CrudTest.Infrastructure.DataBase.Events
{
    public class SqlOutBoxEventItemRepository : IOutBoxEventItemRepository
    {
        private readonly Mc2Mc2CrudTestFrameworkConfiguration _configurations;

        public SqlOutBoxEventItemRepository(Mc2Mc2CrudTestFrameworkConfiguration configurations)
        {
            _configurations = configurations;
        }

        public List<OutBoxEventItem> GetOutBoxEventItemsForPublish(int maxCount = 100)
        {
            using var connection = new SqlConnection(_configurations.PoolingPublisher.SqlOutBoxEvent.ConnectionString);
            var query = string.Format(_configurations.PoolingPublisher.SqlOutBoxEvent.SelectCommand, maxCount);
            var result = connection.Query<OutBoxEventItem>(query).ToList();
            return result;
        }

        public void MarkAsRead(IEnumerable<OutBoxEventItem> outBoxEventItems)
        {
            var idForMark = string.Join(',',
                outBoxEventItems.Where(c => c.IsProcessed).Select(c => c.OutBoxEventItemId).ToList());
            var query = string.Format(_configurations.PoolingPublisher.SqlOutBoxEvent.UpdateCommand, idForMark);

            if (string.IsNullOrWhiteSpace(idForMark)) return;

            using var connection = new SqlConnection(_configurations.PoolingPublisher.SqlOutBoxEvent.ConnectionString);
            connection.Execute(query);
        }
    }
}