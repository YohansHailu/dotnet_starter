// add query to delete comment
using MediatR;
using Blog.Context;
public class DeleteComment : IRequest
{
    public int Id { get; set; }
    public DeleteComment(int id)
    {
        Id = id;
    }
}

// add handler to delete comment

public class DeleteCommentHandler : IRequestHandler<DeleteComment>
{
    private readonly BlogDbContext _context;

    public DeleteCommentHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteComment request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FindAsync(request.Id);
        if (comment == null)
        {
            throw new Exception("Could not find comment");
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
}
