using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// Содержит объект для пагинации
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Содержит объект для пагинации
        /// </summary>
        public Paging() { }

        /// <summary>
        /// Содержит объект для пагинации
        /// </summary>
        /// <param name="shopPages">Число страниц навигации</param>
        public Paging(int? shopPages = null)
        {
            ShowPages = shopPages ?? 3;
        }

        /// <summary>
        /// Содержит объект для пагинации
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="pageItems">Количество элементов на страницке</param>
        /// <param name="shopPages">Число страниц навигации</param>
        public Paging(int page, int pageItems, int? shopPages = null)
        {
            Page = page;
            PageItems = pageItems;
            ShowPages = shopPages ?? 3;
        }

        /// <summary>
        /// Число страниц навигации
        /// </summary>
        public int ShowPages { get; private set; } = 3;

        #region Переменные
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int PageItems { get; set; } = 5;
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// Всего записей на всех страницах
        /// </summary>
        public int Items { get; set; }
        #endregion

        #region Расчетные параметры
        /// <summary>
        /// Количество страниц
        /// </summary>
        public int Pages
        {
            get
            {
                if (PageItems <= 0) return 0;
                else return (int)Math.Ceiling((double)Items / PageItems);//в большую сторону
            }
        }

        /// <summary>
        /// показываемые страницы навигации
        /// </summary>
        public int[] Period
        {
            get
            {
                List<int> returnPeriod = new List<int>();
                var periodShow = (int)Math.Ceiling((double)this.Page / this.ShowPages);
                var periodStart = ((periodShow - 1) * this.ShowPages) + 1;
                var periodEnd = (periodShow * this.ShowPages) < this.Pages ? periodShow * this.ShowPages : this.Pages;
                for (int i = periodStart; i <= periodEnd; i++)
                    returnPeriod.Add(i);
                return returnPeriod.ToArray();
            }
        }

        /// <summary>
        /// Изменяет параметры Paging-а согласно количеству записей
        /// </summary>
        /// <param name="itemsCount">Количество элементво всего</param>
        /// <returns>Элементы с учетом пейджинга</returns>
        public int CalculateSkip(int itemsCount)
        {
            if (Page == 0)
                Page = 1;
            Items = itemsCount;
            int maxPages = (int)Math.Ceiling((double)Items / PageItems);
            if (Page > maxPages) Page = maxPages;
            var pageSkip = Page - 1;
            var skip = pageSkip * PageItems;
            if (skip < 0) skip = 0;

            return skip;
        }
        #endregion
    }
}
