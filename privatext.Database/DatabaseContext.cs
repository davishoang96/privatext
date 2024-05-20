using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using privatext.Database.Models;

namespace privatext.Database;

public class DatabaseContext : DbContext
{
    private SqliteConnection connection;
    private string dbFile = "privatext.db";

    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) { }
    public DatabaseContext(string databaseFile)
    {
        if (!string.IsNullOrEmpty(databaseFile))
        {
            dbFile = databaseFile;
        }
    }

    public DatabaseContext(SqliteConnection sqliteConnection)
    {
        if (!string.IsNullOrEmpty(sqliteConnection?.DataSource))
        {
            dbFile = sqliteConnection.DataSource;
        }

        connection = sqliteConnection;
    }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasKey(s => s.MessageId);
        modelBuilder.Entity<Message>()
            .HasIndex(s => s.MessageId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        connection ??= InitializeSQLiteConnection(dbFile);
        optionsBuilder.UseSqlite(connection);
    }

    private static SqliteConnection InitializeSQLiteConnection(string databaseFile)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databaseFile,
            Password = "@j32[f09q;4gq43Q#$VC"
        };
        return new SqliteConnection(connectionString.ToString());
    }
}
