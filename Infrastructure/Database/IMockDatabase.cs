using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public interface IMockDatabase
    {
        List<Dog> Dogs { get; set; }
        List<Cat> Cats { get; set; }
       
    }
}
