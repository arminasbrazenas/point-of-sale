namespace PointOfSale.DataAccess.OrderManagement.Extensions;

public static class OrderExtensions
{
    public static DateTimeOffset TrimMilliseconds(this DateTimeOffset v)
    {
        return new DateTimeOffset(v.Year, v.Month, v.Day, v.Hour, v.Minute, v.Second, v.Offset);
    }
}
