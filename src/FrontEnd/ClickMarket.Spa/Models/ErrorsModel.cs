namespace ClickMarket.Spa.Models;

public class RetornoViewModel
{
    public bool Success { get; set; }
    public object Data { get; set; }
    public string[] Errors { get; set; }
}
