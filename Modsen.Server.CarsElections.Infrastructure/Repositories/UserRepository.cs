﻿using Modsen.Server.CarsElections.Domain.Entities;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using Modsen.Server.CarsElections.Infrastructure.Data;
using Modsen.Server.CarsElections.Infrastructure.Repositories.Base;

namespace Modsen.Server.CarsElections.Infrastructure.Repositories
{
    internal class UserRepository(DBContext context) : RepositoryBase<User>(context), IUserRepository
    {
    }
}
