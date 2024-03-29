﻿namespace ZUPWebAPI.Entities
{
    public class EmployeeEntity
    {
        public int idEmployee { get; set; }
        public string? nEmployeeShort { get; set; }
        public string? nEmployee { get; set;}
        public string? ActualAddress { get; set; }
        public string? RegisteredAddress { get; set;}
        public string? eMail { get; set; }
        public string? TIN { get; set; }
        public int idPost { get; set; }
        public int idUnit { get; set; }
        public string? PassportNumbers { get; set; }
        public string? PassportIssued { get; set; }
        public DateTime? PassportDate { get; set; } = DateTime.Parse("0001-01-01T00:00:00");
        public DateTime? DateOfEmloyment { get; set; } = DateTime.Parse("0001-01-01T00:00:00");
        public string? TypeSalaryIssue { get; set; }
    }
}
