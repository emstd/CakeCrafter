namespace CakeCrafter.Core.Pages
{
    public class ItemsPage<T>
    {
        public T[] Items { get; set; }
        public int TotalItems {  get; set; }
    }
}
