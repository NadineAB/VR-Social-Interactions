
/*
 * This is the interface for all the classes supplying data to a filter when it's working in auto-update mode
 */

namespace Vatio.Filters
{
    public interface IFilterInput<T>
    {
        //Function that returns input value for the filter, override it and return the input value
        T Get();
    }
}