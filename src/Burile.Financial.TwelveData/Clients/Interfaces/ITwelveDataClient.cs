namespace Burile.Financial.TwelveData.Clients.Interfaces;

public interface ITwelveDataClient
{
    Task<string> GetEtfsAsync();
}