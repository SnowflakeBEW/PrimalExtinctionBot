using System.Threading.Tasks;

namespace PrimalExtinction.Commands
{
    internal interface IItemService
    {
        Task GetItemByName(string name);
    }
}