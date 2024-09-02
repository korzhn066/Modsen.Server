using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsElection.Domain.Entities
{
    public class Car
    {
        public string Id { get; set; } = null!;

        public virtual List<Comment> Comments { get; set; } = new(); 
    }
}
