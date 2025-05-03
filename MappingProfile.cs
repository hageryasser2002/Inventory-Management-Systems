using AutoMapper;
using InventorySystem.Models;
using InventorySystem.Models.Reports;
using System.Transactions;

namespace InventorySystem
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryTransaction, ArchieveTransaction>();
        }

    }
}
