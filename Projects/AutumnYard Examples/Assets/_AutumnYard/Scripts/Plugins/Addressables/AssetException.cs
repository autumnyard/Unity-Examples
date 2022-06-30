using System;

namespace AutumnYard.Core.Assets
{
    public class AssetException : Exception
    {
        public AssetException()
        {

        }

        public AssetException(string message) : base(message)
        {

        }

        public AssetException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}