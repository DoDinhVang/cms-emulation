using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Domain
{
    public interface IEntity<TKey>
    {
        TKey GetId();
        void SetId(TKey id);
    }
}
