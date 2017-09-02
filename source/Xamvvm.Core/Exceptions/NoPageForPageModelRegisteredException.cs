using System;

namespace Xamvvm
{
    public class NoPageForPageModelRegisteredException : Exception
    {
        public NoPageForPageModelRegisteredException(string message) : base(message)
        {
        }
    }
}
