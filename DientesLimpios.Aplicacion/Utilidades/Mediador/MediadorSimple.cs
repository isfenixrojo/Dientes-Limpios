using DientesLimpios.Aplicacion.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public MediadorSimple(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));   
            var casoDeUso = _serviceProvider.GetService(tipoCasoDeUso);

            if (casoDeUso is null)
            { 
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}");
            }

            var metodo = tipoCasoDeUso.GetMethod("Handle")!;   
            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;  
        }
    }
}
