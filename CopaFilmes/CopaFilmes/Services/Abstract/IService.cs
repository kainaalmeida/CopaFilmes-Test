using CopaFilmes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Services.Abstract
{
    public interface IService
    {
        Task<List<Filmes>> GetFilmes();
    }
}
