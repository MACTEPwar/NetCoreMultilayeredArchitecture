namespace BLL
{
    /// <summary>
    /// Содержит типы сортироваки
    /// </summary>
    public class SortType
    {
        /// <summary>
        /// Содержит типы сортировки
        /// </summary>
        public SortType() { }

        private SortType(string value) { Value = value; }

        public string Value { get; set; }

        /// <summary>
        /// Соритровка по возростанию
        /// </summary>
        public static SortType Ascending { get => new SortType("ASC"); }
        /// <summary>
        /// Сортировка по убыванию
        /// </summary>
        public static SortType Descending { get => new SortType("DESC"); }
        /// <summary>
        /// Переопределенный оператор равенства
        /// </summary>
        /// <param name="c1">Значение 1</param>
        /// <param name="c2">Значение 2</param>
        /// <returns></returns>
        public static bool operator ==(SortType c1, SortType c2)
        {
            return c2.Value == c1.Value;
        }
        /// <summary>
        /// Переопределенный оператор не равенства
        /// </summary>
        /// <param name="c1">Значение 1</param>
        /// <param name="c2">Значение 2</param>
        /// <returns></returns>
        public static bool operator !=(SortType c1, SortType c2)
        {
            return c2.Value != c1.Value;
        }
    }

    /// <summary>
    /// Содержит объект соритровки
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string SortField { get; set; }
        /// <summary>
        /// Тип сортировки
        /// </summary>
        public SortType SortType { get; set; }
        /// <summary>
        /// Определяет пустой ли объект
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return SortField == null || string.IsNullOrEmpty(SortType.Value) || SortType.Value == "undefined";
        }
    }
}
