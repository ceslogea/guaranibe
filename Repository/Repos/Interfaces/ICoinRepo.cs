using Models;

namespace Repository.Repos
{
    public interface ICoinRepo : IBaseRepo<RootCoin>
    {
        RootCoin Last();
    }
}