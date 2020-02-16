// ReSharper disable once CheckNamespace
namespace Demelain.Server.Repositories
{
    public interface IRepositoryWrapper
    {
        IPersonalDetailsRepository PersonalDetails { get; }
    }
}