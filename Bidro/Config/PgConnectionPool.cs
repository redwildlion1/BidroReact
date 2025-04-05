using System.Data;
using Npgsql;

namespace Bidro.Config;

public class PgConnectionPool(string connectionString, int maxConnections = 10) : IDisposable
{
    private readonly List<ConnectionSlot> _connections = new();
    private readonly Lock _lock = new();

    public void Dispose()
    {
        foreach (var slot in _connections)
        {
            slot.Semaphore.Dispose();
            slot.Connection.Dispose();
        }
    }

    public async Task<IDbConnection> RentAsync()
    {
        while (true)
        {
            // Try existing connections first
            lock (_lock)
            {
                foreach (var slot in _connections.Where(slot => slot.Semaphore.Wait(0)))
                    return new LeasedConnection(slot, Release);
            }

            // Add a new one if under limit
            lock (_lock)
            {
                if (_connections.Count < maxConnections)
                {
                    var conn = new NpgsqlConnection(connectionString);
                    conn.Open(); // open once and reuse
                    var slot = new ConnectionSlot(conn);
                    slot.Semaphore.Wait();
                    _connections.Add(slot);
                    return new LeasedConnection(slot, Release);
                }
            }

            await Task.Delay(25); // wait and retry
        }
    }

    private void Release(ConnectionSlot slot)
    {
        slot.Semaphore.Release();
    }

    private class ConnectionSlot(NpgsqlConnection connection)
    {
        public NpgsqlConnection Connection { get; } = connection;
        public SemaphoreSlim Semaphore { get; } = new(1, 1);
    }

    private class LeasedConnection(ConnectionSlot slot, Action<ConnectionSlot> release) : IDbConnection
    {
        public void Dispose()
        {
            release(slot);
        }

        public string ConnectionString
        {
            get => slot.Connection.ConnectionString;
            set => slot.Connection.ConnectionString = value;
        }

        public int ConnectionTimeout => slot.Connection.ConnectionTimeout;
        public string Database => slot.Connection.Database;
        public ConnectionState State => slot.Connection.State;

        public IDbTransaction BeginTransaction()
        {
            return slot.Connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return slot.Connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            slot.Connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            slot.Connection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return slot.Connection.CreateCommand();
        }

        public void Open()
        {
            slot.Connection.Open();
        }
    }
}