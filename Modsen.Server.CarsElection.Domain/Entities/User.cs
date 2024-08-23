using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual List<Like> Likes { get; set; } = new();
    }
}
