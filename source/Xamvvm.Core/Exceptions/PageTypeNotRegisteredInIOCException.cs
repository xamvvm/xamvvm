using System;

namespace Xamvvm
{
    public class PageTypeNotRegisteredInIOCException : XamvvmException
    {
        public PageTypeNotRegisteredInIOCException(string message) : base(message)
        {
        }
    }
}