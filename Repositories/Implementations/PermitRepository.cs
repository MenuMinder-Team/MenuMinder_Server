using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class PermitRepository : IPermitRepository
    {
        private readonly Menu_minder_dbContext _context;

        public PermitRepository(Menu_minder_dbContext context)
        {
            _context = context;
        }

        public async Task createBulkPermits(Permit[] permits)
        {
            if(permits.Length > 0)
            {
                foreach(Permit permit in permits)
                {
                    this._context.Permits.Add(permit);
                }
                    await this._context.SaveChangesAsync();
            }
        }
    }
}
