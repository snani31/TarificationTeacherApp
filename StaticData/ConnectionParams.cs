using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.StaticData
{
    public class ConnectionParams
    {

        public string UserName { get; set; } = "";
        public string Port { get; set; } = "";
        public string Database { get; set; } = "";
        public string HostDomein { get; set; } = "";
        public string UserPassword { get; set; } = "";


        public string ReturnConnStr()
        {
            return $"server={HostDomein};port={Port};user={UserName};database={Database};password={UserPassword};";
        }
    }
}
