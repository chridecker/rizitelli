using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Dtos.Base
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
