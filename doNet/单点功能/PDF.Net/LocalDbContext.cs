using PWMIS.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDF.Net
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(): base("connStr")
        {
        }

        protected override bool CheckAllTableExists()
        {

            return true;
        }
    }
}
