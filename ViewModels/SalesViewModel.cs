public class ProductCountByCountry
{
    private string _country;
    private int _count;

    public ProductCountByCountry(string country, int count)
    {
        _country = country;
        _count = count;
    }

    public string Country { get { return _country; } }
    public int Count { get { return _count; } }
}