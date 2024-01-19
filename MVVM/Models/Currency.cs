﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MVVM.Models
{
    /// <summary>
    /// класс который получает Полный перечень иностранных валют,
    /// по отношению к которым Национальным банком устанавливается
    /// официальный курс белорусского рубля
    /// </summary>
    public class Currency : BaseVM
    {
        [Key]
        public int Cur_ID { get; set; }
        public Nullable<int> Cur_ParentID { get; set; }
        public string Cur_Code { get; set; }
        public string Cur_Abbreviation { get; set; }
        public string Cur_Name { get; set; }
        public string Cur_Name_Bel { get; set; }
        public string Cur_Name_Eng { get; set; }
        public string Cur_QuotName { get; set; }
        public string Cur_QuotName_Bel { get; set; }
        public string Cur_QuotName_Eng { get; set; }
        public string Cur_NameMulti { get; set; }
        public string Cur_Name_BelMulti { get; set; }
        public string Cur_Name_EngMulti { get; set; }
        public int Cur_Scale { get; set; }
        public int Cur_Periodicity { get; set; }
        public DateTime Cur_DateStart { get; set; }
        public DateTime Cur_DateEnd { get; set; }
    }
}