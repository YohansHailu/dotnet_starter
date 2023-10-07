using Blog.Controllers;
using Blog.Context;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace test_dir;

public class PostControllerTest
{

    private PostController _controller;
    private DbContextOptions<BlogDbContext> _dbContextOptions;
    Post[] MockPosts = new Post[]{ new Post { Title = "Post 1", Body = "Content 1" },
              new Post { Title = "Post 2", Body = "Content 2" },
              new Post { Title = "Post 3", Body = "Content 3" },
              new Post { Title = "Post 4", Body = "Content 4" } };

    public PostControllerTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var dbContext = new BlogDbContext(_dbContextOptions))
        {
            // drop everything on the database
            dbContext.Database.EnsureDeleted();

            dbContext.Posts.AddRange(MockPosts);

            dbContext.SaveChanges();
            _controller = new PostController(new BlogDbContext(_dbContextOptions));
        }
    }

    [Fact]
    public async Task GetEntities_ReturnsAllEntities()
    {
        // Act
        var result = await _controller.GetPosts();

        // Assert
        Assert.NotNull(result);

        // check if its OK result
        var okayResult = Assert.IsType<OkObjectResult>(result.Result);

        // check if the result contains the list of Post entities
        Assert.NotNull(okayResult.Value);
        var entities = Assert.IsAssignableFrom<IEnumerable<Post>>(okayResult.Value);
        // length of Posts
        Assert.Equal(MockPosts.Length, entities.Count());

    }

    [Fact]
    public async Task GetEntity_ReturnsEntity_WhenIdExists()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _controller.GetPost(id);

        // Assert
        Assert.NotNull(result);

        // check if its OK result
        var okayResult = Assert.IsType<OkObjectResult>(result.Result);

        // check if the result contains the Post entity
        Assert.NotNull(okayResult.Value);
        var entity = Assert.IsAssignableFrom<Post>(okayResult.Value);
        // check if the Post entity has the same id as the one we passed
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public async Task GetEntity_ReturnsNotFound_WhenIdDoesNotExist()
    {
        // Arrange
        var id = MockPosts.Length + 1000;

        // Act
        var result = await _controller.GetPost(id);

        // Assert
        Assert.NotNull(result);

        // check if its NotFound result
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostEntity_ReturnsCreatedResponse()
    {
        // Arrange
        var newPost = new Post { Title = "Post 5", Body = "Content 5" };

        // Act
        var result = await _controller.PostPost(newPost);

        // Assert
        Assert.NotNull(result);

        // check if its CreatedAtAction result
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        // check if the result contains the Post entity
        Assert.NotNull(createdResult.Value);
        var entity = Assert.IsAssignableFrom<Post>(createdResult.Value);
        // check if the Post entity has the same title as the one we passed
        Assert.Equal(newPost.Title, entity.Title);
        Assert.Equal(newPost.Body, entity.Body);

    }

    // test for missing Title for the past 
    [Fact]
    public async Task PostEntity_ReturnsBadRequest_WhenTitleIsMissing()
    {
        // Arrange
        var newPost = new Post { Body = "Content 5" };

        // Act
        var result = await _controller.PostPost(newPost);

        // Assert
        Assert.NotNull(result);

        // check if its BadRequest result
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    // test for missing Body for the post 
    [Fact]
    public async Task PostEntity_ReturnsBadRequest_WhenBodyIsMissing()
    {
        // Arrange
        var newPost = new Post { Title = "Post 5" };

        // Act
        var result = await _controller.PostPost(newPost);

        // Assert
        Assert.NotNull(result);

        // check if its BadRequest result
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
    // when both the Body and Titile are messing
    [Fact]
    public async Task PostEntity_ReturnsBadRequest_WhenBodyAndTitleAreMissing()
    {
        // Arrange
        var newPost = new Post { };

        // Act
        var result = await _controller.PostPost(newPost);

        // Assert
        Assert.NotNull(result);

        // check if its BadRequest result
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }


    [Fact]
    public async Task PutEntity_ReturnsNoContent_WhenIdExists()
    {
        // Arrange
        var id = 1;
        var newPost = new Post { Title = "Post 5", Body = "Content 5" };

        // Act
        var result = await _controller.PutPost(id, newPost);

        // Assert
        Assert.NotNull(result);

        // check if its NoContent result
        Assert.IsType<NoContentResult>(result);
    }


    // write all possible tests for deleteing methods
    [Fact]
    public async Task DeleteEntity_ReturnsNoContent_WhenIdExists()
    {
        // Arrange
        var id = 1;
        // check if id exists
        var post = await _controller.GetPost(id);
        Assert.NotNull(post);


        // Act
        var result = await _controller.DeletePost(id);

        // Assert
        Assert.NotNull(result);

        // check if its NoContent result
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteEntity_ReturnsNotFound_WhenIdDoesNotExist()
    {
        // Arrange
        var id = MockPosts.Length + 1000;

        // Act
        var result = await _controller.DeletePost(id);

        // Assert
        Assert.NotNull(result);

        // check if its NotFound result
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetComments_ReturnsAllComments_WhenIdExists()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _controller.GetComments(id);

        // Assert
        Assert.NotNull(result);

        // check if its OK result
        var okayResult = Assert.IsType<OkObjectResult>(result.Result);

        // check if the result contains the list of Post entities
        Assert.NotNull(okayResult.Value);
        var entities = Assert.IsAssignableFrom<IEnumerable<Comment>>(okayResult.Value);
        // length of Posts

    }

    [Fact]
    public async Task GetComments_ReturnsNotFound_WhenIdDoesNotExist()
    {
        // Arrange
        var id = MockPosts.Length + 1000;

        // Act
        var result = await _controller.GetComments(id);

        // Assert
        Assert.NotNull(result);

        // check if its NotFound result
        Assert.IsType<NotFoundResult>(result.Result);
    }

}
