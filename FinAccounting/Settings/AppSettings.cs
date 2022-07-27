using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinAccounting
{
    public static class AppSettings
    {
        public static DatabaseSettings? DBSettings { get; private set; }
        public static APISettings? APISettings { get; private set; }

        public static void SetSettings(string configPath)
        {
            string configFullPath = Path.GetFullPath(configPath);
            try
            {
                using (StreamReader fileReader = new StreamReader(configFullPath))
                {
                    string json = fileReader.ReadToEnd();
                    JObject settings = JObject.Parse(json);

                    if (!settings.ContainsKey("database"))
                    {
                        throw new NullReferenceException("database");
                    }
                    if (!settings.ContainsKey("api_token"))
                    {
                        throw new NullReferenceException("api_token");
                    }

                    DBSettings = JsonConvert.DeserializeObject<DatabaseSettings>(settings["database"].ToString());
                    APISettings = JsonConvert.DeserializeObject<APISettings>(settings["api_token"].ToString());
                }
                CheckDatabaseSettings();
                CheckAPISettings();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл \"{configFullPath}\" не был найден.");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Ошибка файла конфигурации: не найден один объект \"{ex.Message}\".");
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine($"Ошибка файла конфигурации: не найден один объект \"{ex.Message}\".");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка работы с файлом {configFullPath}: {ex.Message}");
            }
        }

        private static bool CheckDatabaseSettings()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(DBSettings.Provider))
            {
                errorMessage += "Не задано поле \"Provider\" объекта \"database\"\r\n";
            }
            if (string.IsNullOrEmpty(DBSettings.Host))
            {
                errorMessage += "Не задано поле \"Host\" объекта \"database\"\r\n";
            }
            if (string.IsNullOrEmpty(DBSettings.Database))
            {
                errorMessage += "Не задано поле \"Database\" объекта \"database\"\r\n";
            }
            if (string.IsNullOrEmpty(DBSettings.Username))
            {
                errorMessage += "Не задано поле \"Username\" объекта \"database\"\r\n";
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
                errorMessage += "Не задано поле \"Proverkacheka\" объекта \"api_token\"\r\n";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new FileLoadException(errorMessage.Trim());
            }
            return true;
        }
    }
}
