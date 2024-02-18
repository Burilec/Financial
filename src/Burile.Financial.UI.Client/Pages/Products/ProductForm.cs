namespace Burile.Financial.UI.Client.Pages.Products;

public sealed class ProductForm
{
    public Guid ApiId { get; set; }
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public string? Exchange { get; set; }
    public string? Country { get; set; }
    public string? MicCode { get; set; }
}