
using System;
using System.Collections.Generic;
    
namespace miniprojeto_samsys.Infrastructure.Helpers{

    public class PaginatedList<T>
    {

        public PaginatedList()
        {
            this.Items = new List<T>();
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Número de registos totais existentes na origem dos dados
        /// </summary>
        public int TotalRecords { get; set; } = 0;

        /// <summary>
        /// Número de registos que existem na listagem 
        /// </summary>
        public int Count
        {
            get => Items.Count;
        }

        /// <summary>
        /// Listagem com os dados
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Tamanho de cada página
        /// </summary>
        public int PageSize { get; set; } = 0;

        /// <summary>
        /// Número de páginas existentes
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (TotalRecords <= 0) return 0;
                return (int)Math.Ceiling((decimal)TotalRecords / PageSize);
            }
        }

        /// <summary>
        /// Página Atual
        /// </summary>
        public int CurrentPage { get; set; } = 0;

        /// <summary>
        /// Se tem uma próxima página
        /// </summary>
        public bool HasNextPage { get => TotalPages > CurrentPage; }

        //Se tem a página anterior
        public bool HasPreviousPage { get => CurrentPage > 1 && TotalPages >= CurrentPage; }
    }
}