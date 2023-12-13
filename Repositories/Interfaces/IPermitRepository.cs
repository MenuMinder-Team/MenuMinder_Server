using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;

namespace Repositories.Interfaces
{
    public interface IPermitRepository
    {
        public Task createBulkPermits(Permit[] permits);
    }
} 
