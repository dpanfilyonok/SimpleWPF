using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWPF.Models;

namespace SimpleWPF.Services.Interfaces;

public interface ITagService
{
    public IEnumerable<Tag> GetTags();
    public Task AddTagAsync(Tag tag);
    public Task DeleteTagAsync(Tag tag);
    public Task UpdateTagAsync(Tag tag);
}