using Microsoft.Data.Sqlite;
using TechHoy.Domain.IODrivers;
using TechHoy.Domain.IODrivers.DriverAddress;

namespace IODriver.SQLiteDB;

public class SQLiteDBDriver : IIODriver
{
    private string _connectionString = "";
    private string _tableName = "";
    private string _keyColumnName = "";
    private string _valueColumnName = "";
    private SqliteConnection _connection = null;
    public bool Connected
    {
        get
        {
            return _connection != null && _connection.State == System.Data.ConnectionState.Open;
        }
    }

    public SQLiteDBDriver()
    {

    }
    public string GetDescription()
    {
        return "SQLite DB driver";
    }

    public async IAsyncEnumerable<BaseIOValue> GetValuesAsync(IEnumerable<BaseIOAddress> addresses, CancellationToken cancellationToken)
    {
        Dictionary<string, decimal> keyValues = new Dictionary<string, decimal>();

        try
        {
            string sqlExpression = "SELECT " + _keyColumnName + ", " + _valueColumnName + " FROM " + _tableName +
            " WHERE " + _keyColumnName + " in (" + string.Join(",", addresses.Select(a => a.Address)) + ")";
            SqliteCommand command = new SqliteCommand(sqlExpression, _connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        keyValues.Add(((long)reader.GetValue(0)).ToString(), (decimal)(double)reader.GetValue(1));
                    }
                }
            }

        }
        catch (Exception ex)
        {
        }
        foreach (var address in addresses)
        {
            if (keyValues.TryGetValue(address.Address, out decimal value))
            {
                yield return new BaseIOAddressValue<decimal>(address, IOAddressValueState.Valide, value);
            }
            else
            {
                yield return new BaseIOAddressValue<decimal>(address, IOAddressValueState.None, value);
            }
        };
    }

    public void Init(IODriverConfig config)
    {
        if (config is null) return;
        if (config.Keys.TryGetValue("ConnectionString", out object? value)) _connectionString = (string)value;
        if (config.Keys.TryGetValue("TableName", out value)) _tableName = (string)value;
        if (config.Keys.TryGetValue("KeyColumnName", out value)) _keyColumnName = (string)value;
        if (config.Keys.TryGetValue("ValueColumnName", out value)) _valueColumnName = (string)value;
    }

    public Task PauseAsync(CancellationToken cancellationToken)
    {
        return StopAsync(cancellationToken);
    }

    public Task ResetAsync(CancellationToken cancellationToken)
    {
        return StartAsync(cancellationToken);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _connection = new SqliteConnection(_connectionString);
            return _connection.OpenAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw ex;
        };
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _connection.CloseAsync();
    }
}
