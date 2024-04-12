﻿namespace ApiCatalogo.Pagination
{
    // Classe para representar os parâmetros de consulta para paginação
    public class PageStudentQueryParams
    {
        // Número máximo de itens por página
        private const int maxPageSize = 30;

        // Nome a ser filtrado (pode ser vazio)
        public string Name { get; set; } = "";
        public int InitialAge { get; set; } = 0;
        public int FinalAge { get; set; } = 999;
        public bool OwnCollege { get; set; } = false;
        public List<string> Interests { get; set; } = new List<string> { "" };

        // Número da página desejada (padrão: 1)
        public int PageNumber { get; set; } = 1;

        // Tamanho da página (número máximo de itens por página)
        private int _pageSize = maxPageSize;
        

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // Limita o tamanho da página ao valor máximo
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
