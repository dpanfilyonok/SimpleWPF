using System.Collections.Generic;

namespace SimpleWPF.ViewModels;

public interface IItemListViewModel
{
    public IDictionary<string, string> ColumnsBindings { get; set; }
}