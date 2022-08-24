using FinAccountingWebService.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace FinAccountingWebService
{
    public static class AppSettings
    {
        public static DatabaseSettings? DBSettings { get; private set; }
        public static APISettings? APISettings { get; private set; }

        public static void SetSettings(IConfiguration configuration)
        {
            try
            {
                DBSettings = new(configuration.GetValue<string>("Database:Host"),
                                 configuration.GetValue<string>("Database:Database"),
                                 configuration.GetValue<string>("Database:Username"),
                                 configuration.GetValue<string>("Database:Password"),
                                 configuration.GetValue<string>("Database:Port"),
                                 configuration.GetValue<string>("Database:Schema"));
                APISettings = new(configuration.GetValue<string>("ApiToken:Proverkacheka"));

                CheckDatabaseSettings();
                CheckAPISettings();

                if (!CheckDatabaseConnection())
                {
                    throw new NpgsqlException("Ошибка подключения к базе данных");
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Ошибка файла конфигурации: не найден объект \"{ex.Message}\".");
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine($"Ошибка файла конфигурации: \"{ex.Message}\".");
            }
        }

        private static bool CheckDatabaseSettings()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(DBSettings.Host))
            {
                errorMessage += "Не задано поле \"Host\" объекта \"Database\"\r\n";
            }
            if (string.IsNullOrEmpty(DBSettings.Database))
            {
                errorMessage += "Не задано поле \"Database\" объекта \"Database\"\r\n";
            }
            if (string.IsNullOrEmpty(DBSettings.Username))
            {
                errorMessage += "Не задано поле \"Username\" объекта \"Database\"\r\n";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new FileLoadException(errorMessage.Trim());
            }
            return true;
        }

        private static bool CheckAPISettings()
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(APISettings.Proverkacheka))
            {
                errorMessage += "Не задано поле \"Proverkacheka\" объекта \"ApiToken\"\r\n";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new FileLoadException(errorMessage.Trim());
            }
            return true;
        }

        private static bool CheckDatabaseConnection()
        {
            return DatabaseProvider.CheckConnection();
        }
    }
}
