namespace Service.External.FtxApi.Domain.Extensions;

public static class StringExtensions
{
    public static string GetBaseAssetFromMarket(this string market)
    {
        return market.Split("/")[0];
    }
    
    public static string GetQuoteAssetFromMarket(this string market)
    {
        return market.Split("/")[1];
    }
}