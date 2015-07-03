namespace Dolby.UAP.Services.Interfaces
{
    using Dolby.UAP.Models;

    public interface IConfigService
    {
        DataSourceMode DataSource();
        string DataSourceFilePath();
    }
}
