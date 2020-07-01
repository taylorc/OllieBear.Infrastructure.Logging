using System.Data;

namespace Infrastructure.Logging
{
    public class LoggingAdditionalColumns
    {
        //ColumnName = "JobName", PropertyName = "JobName", DataType = SqlDbType.NVarChar, DataLength = 150
        public string ColumnName { get; set; }
        public string PropertyName { get; set; }
        public SqlDbType SqlDbType { get; set; }

        public int DataLength { get; set; }
        
    }
}