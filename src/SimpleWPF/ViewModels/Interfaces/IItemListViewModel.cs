using System.Collections.Generic;

namespace SimpleWPF.ViewModels.Interfaces;

public interface IItemListViewModel
{
    public IDictionary<string, string> ColumnsBindings { get; set; }
}