
public static class SQLiteRunner
{
    public static void Run(string dbPath)
    {
        var dbHandler = new SqlitePersistenceService<Stock>(dbPath);
        var stock1 = new Stock
        {
            Symbol = "MSFT"
        };

        var stock2 = new Stock
        {
            Symbol = "ANSS"
        };

        dbHandler.Post(stock1);
        dbHandler.Post(stock2);


        var stocks = dbHandler.GetAll();
        foreach(var stock in stocks)
        {
            Console.WriteLine($"Id: {stock.Id} | Symbol: {stock.Symbol}");
        }
    }

    public static async Task RunAsync(string dbPath)
    {
        var dbHandler = new SqlitePersistenceService<Stock>(dbPath);
        var stock1 = new Stock
        {
            Symbol = "MSFT"
        };

        var stock2 = new Stock
        {
            Symbol = "ANSS"
        };

        await dbHandler.PostAsync(stock1);
        await dbHandler.PostAsync(stock2);

        var stocksAsync = await dbHandler.GetAllAsync();
        foreach(var stock in stocksAsync)
        {
            Console.WriteLine($"Id: {stock.Id} | Symbol: {stock.Symbol}");
        }
    }
}