using ISI_TP2.Enums;
namespace ISI_TP2.Models
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string Name { get; set; }
        public string CartaConducao { get; set; }
        public int Telefone { get; set; }
        public int token { get; set; }
        public int password { get; set; }

        public Role role { get; set; }

    }
}