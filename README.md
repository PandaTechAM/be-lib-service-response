# ServiceResponse

## Intro
 This is service response template for .Net 6+ web api projects. This template is based on the best practices and
 has goal to harmonize all API I/O operations. The benefit of this template over other templates is that it totally
 integrates with OpenAPI and Swagger. So, IActionResults and other services,
 response will be visible in Swagger UI and loads of other features are and is going to be included.
  
 This Template is designed by PandaTech LLC.
 We build software with the greatest quality!
 Our website: www.pandatech.it :)

 ---

 ## Example
 ### Model
 ```cs
public class Blog
    {
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string PostDescription { get; set; }
        public int BlogId { get; set; }
    }
```

### Program.cs extract
 ```cs
builder.Services.AddTransient<IService, Services>();

#if DEBUG
    builder.Services.AddTransient<IExceptionHandler, DebugExceptionHandler>();
#else
    builder.Services.AddTransient<IExceptionHandler, PublicExceptionHandler>();
#endif
```

 ### Service
 ```cs
public class Services : IService
{
    public ServiceResponse DeletePost(int postId)
    {
        var serviceResponse = new ServiceResponse();

        if (postId == 0) // just for example, cannot be in real case
        {
            serviceResponse.ResponseStatus = ServiceResponseStatus.NotFound;
            serviceResponse.Message = $"Post with id {postId} not found";
            serviceResponse.Success = false;
        }
        else
        {
            serviceResponse.Message = $"Post with id {postId} deleted";
            serviceResponse.ResponseStatus = ServiceResponseStatus.Ok;
        }

        return serviceResponse;
    }

    public ServiceResponse<Post> GetPost(int postId)
    {
        var serviceResponse = new ServiceResponse<Post>();


        serviceResponse.Data = new Post
        {
            PostId = postId,
            PostName = "Your post name",
            PostDescription = "Post Description",
        };

        return serviceResponse;
    }

    public ServiceResponsePaged<List<Blog>> GetAllBlogs(int page, int pageSize)
    {
        var serviceResponse = new ServiceResponsePaged<List<Blog>>();

        serviceResponse.Page = page;
        serviceResponse.PageSize = pageSize;
        serviceResponse.TotalCount = 1; // just for example

        serviceResponse.Data = new List<Blog>
        {
            new Blog
            {
                BlogId = 1,
                BlogName = "Your blog name",
                BlogDescription = "Blog Description",
            }
        };

        return serviceResponse;
    }
}
```
 ### Controller
 ```cs
[ApiController]
[Route("[controller]")]
public class DemoController : ExtendedController
{
    private IService _service;

    public DemoController(IExceptionHandler exceptionHandler, IService service) : base(exceptionHandler)
    {
        _service = service;
    }

    [HttpDelete("Post")]
    public ServiceResponse DeletePost(int postId)
    {
        try
        {
            return _service.DeletePost(postId);
        }
        catch (Exception e)
        {
            return ExceptionHandler.Handle(new ServiceResponse(), e);
        }
    }

    [HttpGet("Post")]
    public ServiceResponse<Post> GetPost(int postId)
    {
        try
        {
            return _service.GetPost(int postId);
        }
        catch (Exception e)
        {
            return ExceptionHandler.Handle(new ServiceResponse<Post>(), e);
        }
    }

    [HttpGet("Blogs")]
    public ServiceResponsePaged<List<Blog>> GetAllBlogs(int page, int pageSize)
    {
        try
        {
            return _service.GetAllBlogs(page,pageSize);
        }
        catch (Exception e)
        {
            return ExceptionHandler.Handle(new ServiceResponsePaged<Blog>(), e);
        }
    }
}
```
