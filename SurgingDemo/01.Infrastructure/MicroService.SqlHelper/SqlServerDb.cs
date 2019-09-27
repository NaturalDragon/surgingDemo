using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace MicroService.SqlHelper
{
    internal class SqlServerDb : DbAbstract
    {
        public override void CombineParams(ref DbCommand command, params object[] parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (!parameter.ParameterName.Contains("@"))
                        parameter.ParameterName = $"@{parameter.ParameterName}";
                    command.Parameters.Add(parameter);
                }


               var sb= new SqlParameter[] { new SqlParameter("", "") };
            }
        }
    }
}
