using Modsen.Server.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.CarsControl.Business.Models.Requests
{
    public class MoveCar
    {
        public string Id { get; set; } = null!;
        public CarType CarType { get; set; }
    }
}
