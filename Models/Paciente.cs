using System.ComponentModel;

namespace ProyectoFinal.Models
{
    public class Paciente
    {
        [DisplayName("Código")]
        public int CodigoPac { get; set; }
        
        [DisplayName("Nombre")]
        public string NombrePac { get; set; }
        
        [DisplayName("Apellido")]
        public string ApellidoPac { get; set; }
        
        [DisplayName("Dirección")]
        public string DireccionPac { get; set; }
        
        [DisplayName("Teléfono")]
        public string TelefonoPac { get; set; }
        
        [DisplayName("Estatus")]
        public string EstatusPac { get; set; }
    }
}
