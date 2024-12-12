using System.Runtime.Serialization;

namespace Soap.Models
{
    [DataContract]
    public class Transporte
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Matricula { get; set; }
        [DataMember]
        public bool ArCondicionado { get; set; }
        [DataMember]
        public decimal TemperaturaAtual { get; set; }
        [DataMember]    
        public int IdUtilizador { get; set; }
    }
}