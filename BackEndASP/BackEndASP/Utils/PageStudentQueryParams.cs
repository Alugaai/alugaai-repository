namespace ApiCatalogo.Pagination
{
    // Classe para representar os parâmetros de consulta para paginação
    public class PageStudentQueryParams
    {

        // Nome a ser filtrado (pode ser vazio)
        public string Name { get; set; } = "";
        public int InitialAge { get; set; } = 0;
        public int FinalAge { get; set; } = 999;
        public bool OwnCollege { get; set; } = false;
        public List<string> Interests { get; set; } = new List<string> { "" };

        // Número da página desejada (padrão: 1)
        public int PageNumber { get; set; } = 1;

        // Tamanho da página (número máximo de itens por página)
        public int PageSize { get; set; } = 10;
    }
}
