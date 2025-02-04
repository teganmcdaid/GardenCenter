using GardenCenter.Model;
using SQLite;

namespace GardenCenter.Services;

public class DatabaseService
{
        private readonly SQLiteAsyncConnection conn;

    public DatabaseService(string dbPath)
    {
        conn = new SQLiteAsyncConnection(dbPath);
        conn.CreateTableAsync<Products>().Wait();
    }

    public Task<List<Products>> GetProductsAsync()
    {
        return conn.Table<Products>().ToListAsync();
    }

    public Task<int> SaveProductAsync(Products product)
    {
        return conn.InsertAsync(product);
    }

    public Task<int> UpdateProductAsync(Products product)
    {
        return conn.UpdateAsync(product);
    }
    public Task<int> DeleteProductAsync(Products product)
    {
        return conn.DeleteAsync(product);
    }
}