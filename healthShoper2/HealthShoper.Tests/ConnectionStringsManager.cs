using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace HealthShoper.Tests;

/// <summary>
/// Менеджер строк подключения для тестов.
/// 
/// Зачем нужен:
/// - Хранит строки подключения в памяти, чтобы можно было быстро получать их по имени.
/// - Позволяет добавлять строки подключения вручную или читать их из конфигурации.
/// - Обеспечивает потокобезопасный доступ к строкам подключения.
/// </summary>

public class ConnectionStringsManager
{
    //создаём внутренний словарь, куда будем складывать пары: название строки подключения - сама строка подключения
    private static readonly IDictionary<string, string> _connectionStrings =
        (IDictionary<string, string>)new Dictionary<string, string>();

    //обычный объект, который нужен только для одного: запирать доступ к словарю, чтобы два потока не записывали туда одновременно.
    private static object _cfgLock = new object();


    public static string DefaultConnectionStringName { get; set; } = "Default";


    //метод для добавления новой строки подключения вручную. Он принимает название строки и саму строку подключения
    public static void Add([NotNull] string key, [NotNull] string connectionString)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        ConnectionStringsManager._connectionStrings[key] = connectionString != null
            ? connectionString
            : throw new ArgumentNullException(nameof(connectionString));
    }


    //Это метод, который читает все строки подключения из файла appsettings.json.
    //Он принимает объект настроек приложения, из которого можно извлечь раздел "ConnectionStrings"
    public static void ReadFromConfiguration([NotNull] IConfiguration configuration)
    {
        //блокируем доступ к словарю, чтобы избежать проблем с потоками
        lock (ConnectionStringsManager._cfgLock)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            //Мы заходим в раздел "ConnectionStrings" внутри файла настроек.Берём каждый элемент внутри него.
            //Записываем в словарь: ключ — название строки подключения
            //значение — сама строка подключения
            foreach (IConfigurationSection child in configuration.GetSection("ConnectionStrings").GetChildren())
                ConnectionStringsManager._connectionStrings[child.Key] = child.Value;
        }
    }

    //метод для получения строки подключения по её названию
    public static string Get([NotNull] string connectionStringName)
    {
        if (connectionStringName == null)
            throw new ArgumentNullException(nameof(connectionStringName));
        string str;
        if (!ConnectionStringsManager._connectionStrings.TryGetValue(connectionStringName, out str))
            throw new KeyNotFoundException("Cannot find connection string '" + connectionStringName + "'");
        //возвращаем найденную строку подключения если она есть
        return str;
    }
}