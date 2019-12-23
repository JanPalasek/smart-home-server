namespace SmartHome.Common
{
    public interface ICloneable<out TType>
    {
        TType Clone();
    }
}