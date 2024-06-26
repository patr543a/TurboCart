﻿using System.Runtime.Serialization;

namespace TurboCart.Infrastructure.Persistance.Repositories
{
    [Serializable]
    internal class AuthenticationException : Exception
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string? message) : base(message)
        {
        }

        public AuthenticationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}