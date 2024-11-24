using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql;

public class FreeSqlCloud : FreeSqlCloud<string>
{
    public FreeSqlCloud() : base(null) { }
    public FreeSqlCloud(string distributeKey) : base(distributeKey) { }
}
