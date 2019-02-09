using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbergueAnimal.Models
{
    /// <summary>
    /// Classe que representa o processo de uma adoção.
    /// </summary>
    public static class ProcessStatusExtension
    {
        /// <summary>
        /// Retorna o estado do processo de adoção.
        /// </summary>
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

    /// <summary>
    /// Enumerado que representa o estado do processo de adoção.
    /// </summary>
    public enum ProcessStatus
    {
        ///<summary>Terminado</summary>
        Finished,
        ///<summary>Cancelado</summary>
        Cancelled,
        ///<summary>Pendente</summary>
        Pending

    }
}
