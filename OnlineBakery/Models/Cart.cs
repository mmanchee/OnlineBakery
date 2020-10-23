namespace OnlineBakery.Models
{
  public class Cart
  {       
    public int LineId { get; set; }
    public int OrderId { get; set; }
    public int FlavorTreatId { get; set; }
    public int Quantity { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual FlavorTreat FlavorTreat { get; set; }
  }
}