﻿using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class SessionExtensions
    {
        private const string sessionKey = "session";

        public static string GetSessionId(this ISession session)
        {
            var sessionId = session.GetString(sessionKey);

            if (sessionId == null)
            {
                sessionId = Guid.NewGuid().ToString();
                session.SetString(sessionKey, sessionId);
            }

            return sessionId;
        }

        public static void SetInt32(this ISession session, string key, int value)
        {
            var bytes = new byte[]
            {
                (byte)(value >> 24),
                (byte)(0xFF & (value >> 16)),
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };
            session.Set(key, bytes);
        }

        public static int? GetInt32(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
            {
                return null;
            }
            return data[0] << 24 | data[1] << 16 | data[2] << 8 | data[3];
        }

        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        public static string GetString(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] Get(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] value);
            return value;
        }
    }
}
