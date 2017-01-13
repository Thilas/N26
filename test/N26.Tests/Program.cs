using System.Linq;
using System.Threading.Tasks;

namespace N26.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var username = args[0];
            var password = args[1];
            Task.Run(async () => await TestAsync(username, password)).Wait();
        }

        private static async Task TestAsync(string username, string password)
        {
            var n26 = await N26.LoginAsync(username, password);
            //var accounts = await n26.GetAccountsAsync();
            //var addresses = await n26.GetAddressesAsync();
            //var cards = await n26.GetCardsAsync();
            //var me = await n26.GetMeAsync();
            //var transactions = await n26.GetTransactionsAsync();
            var transactionsQuery = n26.Transactions;
            var transactionsResult = transactionsQuery.Where(t => t.Amount > 100).ToList();
        }
    }
}
