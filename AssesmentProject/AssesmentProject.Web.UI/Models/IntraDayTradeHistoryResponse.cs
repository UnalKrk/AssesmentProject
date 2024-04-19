using Newtonsoft.Json;

public class IntraDayTrade
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("conract")]
    public string Conract { get; set; }

    [JsonProperty("price")]
    public double Price { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}

public class Statistics
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("priceWeightedAverage")]
    public double PriceWeightedAverage { get; set; }

    [JsonProperty("priceMin")]
    public double PriceMin { get; set; }

    [JsonProperty("priceMax")]
    public double PriceMax { get; set; }

    [JsonProperty("quantityMin")]
    public int QuantityMin { get; set; }

    [JsonProperty("quantityMax")]
    public int QuantityMax { get; set; }

    [JsonProperty("quantitySum")]
    public int QuantitySum { get; set; }
}

public class Body
{
    [JsonProperty("intraDayTradeHistoryList")]
    public List<IntraDayTrade> IntraDayTradeHistoryList { get; set; }

    [JsonProperty("statistics")]
    public List<Statistics> Statistics { get; set; }
}

public class ApiResponse
{
    [JsonProperty("resultCode")]
    public string ResultCode { get; set; }

    [JsonProperty("resultDescription")]
    public string ResultDescription { get; set; }

    [JsonProperty("body")]
    public Body Body { get; set; }
}