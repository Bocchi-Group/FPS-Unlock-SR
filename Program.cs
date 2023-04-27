using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FPSUnlock
{
    internal class FPS
    {
        public static void Main()
        {
            var node = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Cognosphere")?.OpenSubKey("Star Rail", true);
            if (node != null)
            {
                var keyName = node.GetValueNames().FirstOrDefault(x => x.StartsWith("GraphicsSettings_Model"));
                if (keyName == null)
                {
                    Console.WriteLine("Failed to get registry content, try changing graphics settings in the game at least once, and try again.");
                    Console.ReadLine();
                    return;
                }
                var key = node.GetValue(keyName);
                if (key == null)
                {
                    Console.WriteLine("Failed to get registry content, try changing graphics settings in the game at least once, and try again");
                    Console.ReadLine();
                    return;
                }
                var value = Encoding.UTF8.GetString((byte[])key);
                var json = JObject.Parse(value);
                Console.WriteLine("Enter your preferred FPS");
                json["FPS"] = Console.ReadLine();
                node.SetValue(keyName, Encoding.UTF8.GetBytes(json.ToString(Newtonsoft.Json.Formatting.None)));
                Console.WriteLine("Successfully patched FPS Unlock");
            }
            Console.ReadLine();
        }
    }
}