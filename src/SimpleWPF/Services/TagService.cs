using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.Services.Interfaces;

namespace SimpleWPF.Services;

public class TagService : ITagService
{
    private readonly ICrudRepository<Tag, int> _tags;

    public TagService(ICrudRepository<Tag, int> tags)
    {
        _tags = tags;
    }
    
    public IEnumerable<Tag> GetTags()
    {
        return _tags.GetAll().ToList();
    }

    public async Task AddTagAsync(Tag tag)
    {
        await _tags.AddAsync(tag);
    }

    public async Task DeleteTagAsync(Tag tag)
    {
        await _tags.DeleteAsync(tag);
    }

    public async Task UpdateTagAsync(Tag tag)
    {
        await _tags.UpdateAsync(tag);
    }
}