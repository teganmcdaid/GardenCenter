using GardenCenter.Model;
using SQLite;

namespace GardenCenter.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection conn;

    public DatabaseService(string dbPath)
    {
        conn = new SQLiteAsyncConnection(dbPath);

        //create table for both user and basket items
        conn.CreateTableAsync<User>().Wait();
        conn.CreateTableAsync<BasketItem>().Wait();
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
    public Task<int> DeleteAllUsersAsync()
    {
        return conn.ExecuteAsync("DELETE FROM User");
    }

    //Basket Methods
    //////////////////////////////////////

    //Get the basket based of the users unique phone number
    public Task<List<BasketItem>> GetUserBasketAsync(string number)
    {
        return conn.Table<BasketItem>().Where(i => i.PhoneNumber == number).ToListAsync();
    }
    public Task<int> AddToBasketAsync(BasketItem item)
    {
        return conn.InsertAsync(item);
    }

    public Task<int> RemoveFromBasketAsync(int itemId)
    {
        return conn.DeleteAsync<BasketItem>(itemId);
    }

    public Task<int> ClearBasketAsync(string number)
    {
        return conn.ExecuteAsync("DELETE FROM BasketItem WHERE PhoneNumber = ?", number);
    }

    public Task<int> UpdateBasketItemAsync(BasketItem item)
    {
        return conn.UpdateAsync(item);
    }

    //decided to use json file for products instead of database
    //if extra time at the end will incorporate database
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