using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;

namespace Repositories.Interfaces
{
  public interface IReservationRepository
  {
    public Task InsertReservation(Reservation reservation);
  }
}
