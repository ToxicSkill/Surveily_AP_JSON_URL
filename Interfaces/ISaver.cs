using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSON_URL.Interfaces
{
    public interface ISaver
    {
        Task<bool> SaveFiles(List<(string data, string fname)> files);
    }
}
