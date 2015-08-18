namespace Dolby.UAP.Services.Interfaces
{
    using Dolby.UAP.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataProvider
    {
        Task<List<Video>> GetDolbyMoviesInfo();
    }
}
