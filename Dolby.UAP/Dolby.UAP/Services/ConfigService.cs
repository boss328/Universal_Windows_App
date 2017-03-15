namespace Dolby.UAP.Services
{
    using Dolby.UAP.Models;
    using Dolby.UAP.Services.Interfaces;
    public class ConfigService : IConfigService
    {
        // TODO: set default values for DataSource files paths (in-app file or url to file) 
        //       and DataSourceMode (DataSourceMode.FromAppFile or DataSourceMode.FromUrl)
        private string _localDataSourceFilePath = @"Data\DataSource.json";
        private string _urlDataSourceFilePath = "http://dataurl.com/DataPath";
        private DataSourceMode _dataSourceMode = DataSourceMode.FromAppFile;

        public DataSourceMode DataSource()
        {
            return _dataSourceMode;
        }

        public string DataSourceFilePath()
        {
            string path = "";

            switch (_dataSourceMode)
            {
                case DataSourceMode.FromAppFile:
                    path = _localDataSourceFilePath;
                    break;
                case DataSourceMode.FromUrl:
                    path = _urlDataSourceFilePath;
                    break;
                default:
                    path = _localDataSourceFilePath;
                    break;
            }

            return path;
        }
    }
}
