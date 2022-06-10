namespace WebApiAutores.Middlewares
{
    public static class LoguearRespuestaHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoguearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
        }
    }
    public class LoguearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> _logger;

        public LoguearRespuestaHTTPMiddleware(RequestDelegate siguiente,
                                              ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        // Una regla para usar esta clase como un middleware es tener un metodo Invoke o InvokeAsync
        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await _siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

                _logger.LogInformation(respuesta);
            }
        }
    }
}
