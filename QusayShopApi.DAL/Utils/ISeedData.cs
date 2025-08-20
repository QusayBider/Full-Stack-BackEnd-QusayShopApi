using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Utils
{
    public interface ISeedData
    {
        public Task DataSeedingAsync();
        public Task IdentityRoleSeedingAsync();
    }
}
