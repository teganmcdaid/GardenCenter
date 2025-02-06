using GardenCenter.Model;
using SQLite;

namespace GardenCenter.Services;

public class DatabaseService
{
        private readonly SQLiteAsyncConnection conn;

    public DatabaseService(string dbPath)
    {
        conn = new SQLiteAsyncConnection(dbPath);

        //create table for both user and products
        conn.CreateTableAsync<User>().Wait();
        //conn.CreateTableAsync<Products>().Wait();
    }

    //User Methods
    //////////////////////////////////////
    public Task<List<User>> GetUserAsync()
    {
        return conn.Table<User>().ToListAsync();
    }

    public Task<int> SaveUserAsync(User user)
    {
        return conn.InsertAsync(user);
    }

    //Product methods
    //////////////////////////////////////
    //public Task<List<Products>> GetProductsAsync()
    //{
    //    return conn.Table<Products>().ToListAsync();
    //}

    //public Task<int> SaveProductAsync(Products product)
    //{
    //    return conn.InsertAsync(product);
    //}

    //public Task<int> UpdateProductAsync(Products product)
    //{
    //    return conn.UpdateAsync(product);
    //}
    //public Task<int> DeleteProductAsync(Products product)
    //{
    //    return conn.DeleteAsync(product);
    //}
}