namespace Mc2.CrudTest.Shared.Configuration.PoolingPublisher
{
    public class PoolingPublisherConfiguration
    {
        public bool Enabled { get; set; }
        public string OutBoxRepositoryTypeName { get; set; }
        public SqlOutBoxEvent SqlOutBoxEvent { get; set; }
        public int SendOutBoxInterval { get; set; }
        public int SendOutBoxCount { get; set; }
    }

    public class SqlOutBoxEvent
    {
        public string ConnectionString { get; set; }
        public string SelectCommand { get; set; }
        public string UpdateCommand { get; set; }
    }
}