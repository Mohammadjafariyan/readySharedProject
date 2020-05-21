using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharedCoreWebApp.ContextFactory;

namespace SharedCoreWebApp.Service
{
    public class GenericTablesService
    {
        private readonly ContextFactoryService _contextFactoryService;
        private AbstractDbContext _context;

        public GenericTablesService(ContextFactoryService contextFactoryService)
        {
            _contextFactoryService = contextFactoryService;
            _context = _contextFactoryService.GetDbContext(null);
        }

        public IList<string> ListTables()
        {
            List<string> tables = new List<string>();
            DataTable dt = _context.Database.GetDbConnection()
                .GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string) row[2];
                tables.Add(tablename);
            }

            return tables;
        }

        public List<string> GetAllTableNames()
        {
            return ListTables().ToList();
        }

        public Dictionary<string, string> SetNames(List<string> list, Dictionary<string, string> tableNamesDictionary)
        {
            Dictionary<string, string> namesDictionary = new Dictionary<string, string>();
            foreach (var table in list)
            {
                var _key = tableNamesDictionary.Keys.FirstOrDefault(k => k == table);

                if (string.IsNullOrEmpty(_key))
                {
                    continue;
                }

                namesDictionary.Add(_key, tableNamesDictionary[_key]);
            }

            return namesDictionary;
        }

        public TableViewModel ReadAllDataWithStruture(string table)
        {
            var sqlText = string.Format("SELECT * FROM {0}", table);

            DataTable dt = new DataTable();

            // Use DataTables to extract the whole table in one hit
            using (SqlDataAdapter da =
                new SqlDataAdapter(sqlText, _context.Database.GetDbConnection().ConnectionString))
            {
                da.Fill(dt);
            }


            return new TableViewModel
            {
                DataTable = dt
            };
        }
    }

    public class TableViewModel
    {
        public string TableName { get; set; }
        public string TableFaName { get; set; }
        public DataTable DataTable { get; set; }
    }

    public class MyGenericTableGlobal
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}