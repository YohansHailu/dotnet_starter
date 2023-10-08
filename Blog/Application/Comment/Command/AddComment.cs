// write command and handler to add a comment to a post
using MediatR;
using Blog.Context;
using Blog.Models;

public class AddComment : IRequest<Comment>
{
    public Comment Comment { get; set; }
    public AddComment(Comment comment)
    {
        Comment = comment;
    }
}

public class AddCommentHandler : IRequestHandler<AddComment, Comment>
{
    private readonly BlogDbContext _context;

    public AddCommentHandler(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> Handle(AddComment request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FindAsync(request.Comment.PostId);
        if (post == null)
        {
            throw new Exception("post is not found");
        }
        _context.Comments.Add(request.Comment);
        await _context.SaveChangesAsync();

        return request.Comment;

    }
}
