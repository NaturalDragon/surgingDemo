using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.EntityFramwork.Data
{
  public  interface IRespositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
