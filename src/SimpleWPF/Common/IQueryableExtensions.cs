using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleWPF.Common;

public static class QueryableExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> query)
    {
        return new ObservableCollection<T>(query);
    }
}