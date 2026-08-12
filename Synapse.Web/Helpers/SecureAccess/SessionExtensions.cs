using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace Synapse.Web.Helpers.SecureAccess
{
    public static class SessionExtensions
    {
        // Store object
        public static void AddItem<T>(this ISession session, T item)
            where T : class
        {
            if (item == null)
                return;

            var jsonString = JsonSerializer.Serialize(item);

            session.SetString(
                GetKey(typeof(T)),
                jsonString);
        }

        // Get object
        public static T? GetItem<T>(this ISession session)
            where T : class
        {
            var jsonString = session.GetString(
                GetKey(typeof(T)));

            if (string.IsNullOrEmpty(jsonString))
                return null;

            return JsonSerializer.Deserialize<T>(jsonString);
        }

        // Update object
        public static void UpdateItem<T>(
            this ISession session,
            T value)
            where T : class
        {
            if (value == null)
                return;

            var jsonString = JsonSerializer.Serialize(value);

            session.SetString(
                GetKey(typeof(T)),
                jsonString);
        }

        // Remove object
        public static void RemoveItem<T>(this ISession session)
            where T : class
        {
            session.Remove(GetKey(typeof(T)));
        }

        // Generate session key from type
        public static string GetKey(Type itemType)
        {
            return itemType.FullName ?? itemType.Name;
        }
    }
}