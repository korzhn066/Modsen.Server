using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElections.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>;
}
