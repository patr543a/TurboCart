using System.Runtime.Serialization;

namespace TurboCart.Infrastructure.Persistance.UnitOfWorks
{
    [Serializable]
    internal class UnitOfWorkException : Exception
    {
        public UnitOfWorkException()
        {
        }

        public UnitOfWorkException(string? message) : base(message)
        {
        }

        public UnitOfWorkException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected UnitOfWorkException(SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore SYSLIB0051 // Type or member is obsolete
        {
        }
    }
}