using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    public static class ProcessStatusExtension
    {
        public static string GetProcessStatusName(this ProcessStatus processStatus)
        {
            switch (processStatus)
            {
                case ProcessStatus.Finished: return "Concluído";
                case ProcessStatus.Cancelled: return "Cancelado";
                default:
                case ProcessStatus.Pending: return "Pendente";
            }
        }

    }
    public enum ProcessStatus
    {
       
            Finished,
            Cancelled,
            Pending
        
    }
}
