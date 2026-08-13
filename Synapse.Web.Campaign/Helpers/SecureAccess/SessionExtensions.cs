using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Synapse.Web.CampaignPlugin.Helpers.SecureAccess
{
    public static class SessionExtensions
    {
        // 1. REPLACED HttpSessionStateBase WITH ISession
        public static void AddItem<T>(this ISession session, T item) where T : class
        {
            if (item == null) return;

            // Convert the complex object into a JSON string to store it
            var jsonString = JsonSerializer.Serialize(item);
            session.SetString(GetKey(item.GetType()), jsonString);
        }

        public static T? GetItem<T>(this ISession session) where T : class
        {
            var jsonString = session.GetString(GetKey(typeof(T)));

            // If the key doesn't exist, return null
            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }

            // Convert the JSON string back into your complex C# class
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public static string GetKey(Type itemType)
        {
            // FullName can theoretically be null for certain types; fallback to Name just in case
            return itemType.FullName ?? itemType.Name;
        }

        public static void UpdateItem<T>(this ISession session, T value) where T : class
        {
            if (value == null) return;

            // In .NET 8 JSON-backed sessions, updating an item simply overwrites the old JSON string
            var jsonString = JsonSerializer.Serialize(value);
            session.SetString(GetKey(typeof(T)), jsonString);
        }
        public static void RemoveItem<T>(this ISession session) where T : class
        {
            var item = GetKey(typeof(T));
            if (item != null)
            {
                session.Remove(item);
            }
        }
    }
}