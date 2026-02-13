using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundException(string email):NotFoundException($"User with Email {email} is not found.")
    {
    }
}
