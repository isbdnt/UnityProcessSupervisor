using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProcessSupervisor;

namespace UnityProcessSupervisor {
    public class UnityProcessInfo {
        public UnityProjectInfo project;
        public Process process;
    }
}
