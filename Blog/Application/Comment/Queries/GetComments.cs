using MediatR;
using Blog.Models;
using Blog.Context;
// you can also use this one.
//public record GetComments() : IRequest<List<Comment>>;
public class GetComments : IRequest<List<Comment>>
{
};

public class GetCommentsHandler : IRequestHandler<GetComments, List<Comment>>
{
    private readonly BlogDbContext _context;

    public GetCommentsHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> Handle(GetComments request, CancellationToken cancellationToken)
    {
        return await _context.Comments.ToListAsync();
    }
}
