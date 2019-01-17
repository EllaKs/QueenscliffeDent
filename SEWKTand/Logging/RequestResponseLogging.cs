using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEWKTand.Logging
{
    public class RequestAndResponseLogging
    {
        private readonly RequestDelegate _next;

        public RequestAndResponseLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            using (StreamWriter sw = new StreamWriter("C:\\Users\\Ella\\Desktop\\AvcProg\\SEWK\\SEWKTand\\SEWKTand\\bin\\Debug\\Logging.txt", true))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine($"Method: {context.Request.Method}");
                sw.WriteLine($"Path: {context.Request.Path}");
            }
        }
    }
}
