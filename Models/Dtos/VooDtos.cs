namespace ProjetoAutenticacaoBasica.Api.Models.Dtos
{


    public class CriarVooDto
    {
        public int Codigo { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

    }

    public class ResumoVooDto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Trajeto { get; set; }

    }

    public class PassageiroNoVooDto
    {
        public string Nome { get; set; }

        public int Assento { get; set; }

    }

    public class VOoManifestoDto 
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Trajeto { get; set; }
        public List<PassageiroNoVooDto> Passageiro { get; set; }
    }
}
