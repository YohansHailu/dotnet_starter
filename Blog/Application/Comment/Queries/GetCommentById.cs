using MediatR;
using Blog.Models;
using Blog.Context;
// you can also use this one.
//public record GetComments() : IRequest<List<Comment>>;

public class GetCommentById : IRequest<Comment>
{
    public int Id { get; set; }
    public GetCommentById(int id)
    {
        Id = id;
    }
};


public class GetCommentByIdHandler : IRequestHandler<GetCommentById, Comment>
{
    private readonly BlogDbContext _context;

    public GetCommentByIdHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> Handle(GetCommentById request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == request.Id);
        return comment;
    }
}
