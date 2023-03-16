# ServiceResponse

## Intro
 This is service response template for .NET 6, 7 web api projects. This template is based on the best practices and
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
        public Blog Blog { get; set; }
    }
```
 ### Service
 ```cs
public class Services : Handler
    {
        public ServiceResponse DeletePost(int postId)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                if (postId == 0) // just for example, cannot be in real case
                {
                    serviceResponse.ResponseStatus = ServiceResponseStatus.NotFound;
                    serviceResponse.Message = $"PostId is with number {postId} not found";
                    serviceResponse.Success = false;
                }
                //Delete post logic for serviceResponse
            }
            catch (Exception serviceException)
            {
                ExceptionHandler(serviceResponse, serviceException);
            }

            return serviceResponse;
        }

        public ServiceResponse<Post> GetPost(int postId)
        {
            var serviceResponse = new ServiceResponse<Post>();
            try
            {
                //Get post logic for serviceResponse
            }
            catch (Exception serviceException)
            {
                ExceptionHandler(serviceResponse, serviceException);
            }

            return serviceResponse;
        }

        public ServiceResponsePaged<List<Blog>> GetAllBlogs(int page, int pagesize)
        {
            var serviceResponse = new ServiceResponsePaged<List<Blog>>();
            try
            {
                //get all blogs logic with pagination for serviceResponse
            }
            catch (Exception serviceException)
            {
                ExceptionHandler(serviceResponse, serviceException);
            }

            return serviceResponse;
        }
    }
```
 ### Controller
 ```cs
 public class Controller : ExtendedController
    {
        [HttpDelete("Post")]
        public ServiceResponse DeletePost(int postId)
        {
            var serviceResponse = DeletePost(postId);

            return SetResponse(serviceResponse);
        }

        [HttpGet("Post")]
        public ServiceResponse<Post> GetPost(int postId)
        {
            var serviceResponse = GetPost(postId);

            return SetResponse(serviceResponse);
        }

        [HttpGet("Blogs")]
        public ServiceResponsePaged<List<Blog>> GetAllBlogs(int page, int pagesize)
        {
            var serviceResponse = GetAllBlogs(page, pagesize);

            return SetResponse(serviceResponse);
        }
    }
```
