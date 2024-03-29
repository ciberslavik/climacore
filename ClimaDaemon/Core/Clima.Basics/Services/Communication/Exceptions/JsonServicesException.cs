﻿using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication.Exceptions
{
    public class JsonServicesException : Exception
    {
        public JsonServicesException(int code, string message, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
        }

        internal JsonServicesException()
            : base(nameof(JsonServicesException))
        {
            // for unit tests
        }

        private static Dictionary<int, Type> ExceptionTypes { get; } = GetExceptionTypes();

        internal static Dictionary<int, Type> GetExceptionTypes()
        {
            var thisType = typeof(JsonServicesException);
            var types =
                from type in thisType.Assembly.GetTypes()
                where type.BaseType == thisType
                select type;

            return types.ToDictionary(t =>
            {
                var errorCodeConstant = t.GetField(nameof(JsonServicesException));
                return (int) errorCodeConstant.GetValue(null);
            }, t => t);
        }

        public static JsonServicesException Create(Error error, string messageId = null)
        {
            if (ExceptionTypes.TryGetValue(error.Code, out var type))
            {
                var result = (JsonServicesException) Activator.CreateInstance(type, new object[] {error});
                result.MessageId = messageId;
                return result;
            }

            return new JsonServicesException(error.Code, error.Message)
            {
                Details = error.Data,
                MessageId = messageId
            };
        }

        private const string CodeKey = nameof(JsonServicesException) + "." + nameof(Code);

        public int Code
        {
            get => Data[CodeKey] != null ? Convert.ToInt32(Data[CodeKey]) : 0;
            set => Data[CodeKey] = value;
        }

        private const string MessageIdKey = nameof(JsonServicesException) + "." + nameof(MessageId);

        public string MessageId
        {
            get => Data[MessageIdKey] as string;
            set => Data[MessageIdKey] = value;
        }

        private const string DetailsKey = nameof(JsonServicesException) + "." + nameof(Details);

        public object Details
        {
            get => Data[DetailsKey];
            set
            {
                if (value == null || value.GetType().IsSerializable)
                    Data[DetailsKey] = value;
                else
                    Data[DetailsKey] = value.ToString();
            }
        }
    }
}