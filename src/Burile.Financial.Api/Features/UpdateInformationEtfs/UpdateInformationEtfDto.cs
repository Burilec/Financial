using Newtonsoft.Json;

namespace Burile.Financial.Api.Features.UpdateInformationEtfs;

internal sealed class UpdateInformationEtfsDto
{
    public List<UpdateInformationEtfDto>? Data { get; set; }
}

internal sealed class UpdateInformationEtfDto
{
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public string? Exchange { get; set; }

    [JsonProperty("mic_code")] public string? MicCode { get; set; }

    public string? Country { get; set; }
}