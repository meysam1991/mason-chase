using System.Collections.Generic;

namespace Mc2.CrudTest.ModelFramework.Events
{
    public interface IOutBoxEventItemRepository
    {
        List<OutBoxEventItem> GetOutBoxEventItemsForPublish(int maxCount = 100);
        void MarkAsRead(IEnumerable<OutBoxEventItem> outBoxEventItems);
    }
}
