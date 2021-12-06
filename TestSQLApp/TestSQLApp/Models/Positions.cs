using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQLApp.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public List<Employee> Employees { get; set; } = new();
    }
}
