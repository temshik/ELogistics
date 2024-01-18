using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVVM.Models
{
    /// <summary>
    /// класс который получает Официальный курс белорусского рубля по отношению к иностранным валютам,
    /// устанавливаемый Национальным банком на конкретную дату
    /// </summary>
    public class Rate
    {
        [Key]
        public int Cur_ID { get; set; }
        public DateTime Date { get; set; }
        public string Cur_Abbreviation { get; set; }
        public int Cur_Scale { get; set; }
        public string Cur_Name { get; set; }
        public decimal? Cur_OfficialRate { get; set; }
    }

    /// <summary>
    /// класс для получения динамики официального курса белорусского рубля
    /// по отношению к заданной иностранной валюте (не более чем за 365 дней)
    /// </summary>
    public class RateShort
    {
        [JsonProperty(PropertyName = "Cur_Id")]
        public int Cur_ID { get; set; }
        [Key]
        [JsonProperty(PropertyName = "Id")]
        public DateTime Date { get; set; }
        public decimal Cur_OfficialRate { get; set; }
    }

}