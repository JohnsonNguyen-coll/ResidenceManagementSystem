using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_PRN.Models;

namespace Assignment_PRN
{
    internal class RegistrationDAO
    {
        public List<Registration> GetRegistration()
        {
            TestPrnassContext context = new TestPrnassContext();
            return context.Registrations.ToList();
        }
    }
}
