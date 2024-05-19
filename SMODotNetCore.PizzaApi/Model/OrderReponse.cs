namespace SMODotNetCore.PizzaApi.Model
{
    public class OrderReponse
    {
        public string Message { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
