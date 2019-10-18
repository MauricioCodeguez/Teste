using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Teste.Extensions
{
    public static class CollectionExtensions
    {
        internal static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> list)
        {
            if (list != null)
                return new ObservableCollection<T>(list);

            return new ObservableCollection<T>();
        }
    }
}