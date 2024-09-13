using Modsen.Server.CarsElections.Application.Specifications.Base;
using Modsen.Server.CarsElections.Domain.Entities;

namespace Modsen.Server.CarsElections.Application.Specifications.CommentSpecifications
{
    internal class CommentIdSpecification(int id) : SpecificationBase<Comment>(Comment => Comment.Id == id)
    {
    }
}
