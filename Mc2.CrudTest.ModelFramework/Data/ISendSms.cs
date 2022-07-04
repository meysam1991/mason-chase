using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Data
{
    public interface ISendSms
    {
        Task SendMessageAsync(string mobileNumber, string message);
    }
}
