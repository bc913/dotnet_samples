
var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BcanMyApp", "finance.db");
var liteRunner = new LiteDbRunner<Client>(dbPath);

var allClients = liteRunner.GetAll();
foreach (var client in allClients)
    Console.WriteLine(client);
Console.WriteLine("=========================");

// var customer = new Client
// { 
//     Name = "hasan basri", 
//     Phones = new string[] { "1234-045", "1340-4567" }, 
//     IsActive = true,
//     OrderItem = new OrderItem{ProductName = "Baller", Quantity = 2, UnitPrice = 3.454m}
// };
//
// liteRunner.InsertOne(customer);
// allClients = liteRunner.GetAll();
// foreach (var client in allClients)
//     Console.WriteLine(client);


//var orderRunner = new LiteDbRunner<Order>(dbPath);
// orderRunner.InsertOne(
//     new Order
//         {
//             OrderDate = DateTime.Today, 
//             TotalAmount = 345.6m, 
//             Items = new List<OrderItem>
//             {
//                 new OrderItem{Quantity = 4, UnitPrice = 34.0m}
//                 
//             }});


// var osman = liteRunner.GetOneByName("Cemil");
// osman.Address =  new Address{City = "Melbourne", Country = "Australia"};
// liteRunner.UpdateOne(osman);

// var allOrders = orderRunner.GetAll();
// foreach (var order in allOrders)
//     Console.WriteLine(order);
