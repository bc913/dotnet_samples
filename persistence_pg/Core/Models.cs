using System;


public abstract class ModelBase
{
    public abstract string TableName { get; }
}

public class Stock : ModelBase
{	
    public Stock()
    {
        
    }
    public int Id { get; set; }	

    public string Symbol { get; set; }

    public override string TableName => "Stocks";
}		
    		

public class Valuation		
{			
    public int Id { get; set; }	
	
    public int StockId { get; set; }
			
    public DateTime Time { get; set; }	
		
    public decimal Price { get; set; }		
}		