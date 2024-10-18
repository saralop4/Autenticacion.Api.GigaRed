using Autenticacion.Web.Api.Transversal.Interfaces;
using Microsoft.Extensions.Logging;  //componente nugget
using WatchDog;

namespace Autenticacion.Web.Api.Transversal.Modelos;
public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;
    public LoggerAdapter(ILoggerFactory loggerFactory) //esta interfaz nos permite crear los sistemas de loggin y trazabilidad de las excepciones
    {
        _logger = loggerFactory.CreateLogger<T>();
    }
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);  //estos metodos son propios del componente nugget que se instaló
        WatchLogger.Log(message);
    }
    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
        WatchLogger.Log(message);

    }
    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
        WatchLogger.Log(message);

    }
}
