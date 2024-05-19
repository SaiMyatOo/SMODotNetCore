namespace SMODotNetCore.PizzaApi.Model
{
    public class OrderRequest
    {
        public int PizzaId;
        public int[] Extras { get; set; }
    }
}