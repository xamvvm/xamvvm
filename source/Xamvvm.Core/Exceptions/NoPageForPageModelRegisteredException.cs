using System;

namespace Xamvvm
{
    public class NoPageForPageModelRegisteredException : XamvvmException
    {
        public NoPageForPageModelRegisteredException(string message) : base(message)
        {
        }
    }
}
