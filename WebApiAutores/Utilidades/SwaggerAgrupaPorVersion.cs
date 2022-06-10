using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiAutores.Utilidades
{
    public class SwaggerAgrupaPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceControlador = controller.ControllerType.Namespace; // = controller.v1
            var versionApi = namespaceControlador.Split(".").Last().ToLower(); // = v1 0 v2
            controller.ApiExplorer.GroupName = versionApi;
        }
    }
}
