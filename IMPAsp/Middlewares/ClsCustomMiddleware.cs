namespace IMPAsp.Middlewares
{
    public class ClsCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public ClsCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Logic before passing the request to the next middleware
            Console.WriteLine($"Request Path: {context.Request.Path}");

            await _next(context); // Call the next middleware

            // Logic after the response is generated
            Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
        }
    }
}
