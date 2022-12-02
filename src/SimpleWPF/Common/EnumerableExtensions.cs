using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleWPF.Common;

public static class EnumerableExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> query)
    {
        return new ObservableCollection<T>(query);
    }
    
    public static string ConcatToString(this IEnumerable collection)
    {
        return string.Join(" ", collection);
    }
}