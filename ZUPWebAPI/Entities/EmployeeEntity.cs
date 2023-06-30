namespace ZUPWebAPI.Entities
{
    public class EmployeeEntity
    {
        public int idEmployee { get; set; }
        public string nEmployeeShort { get; set; }
        public string nEmployee { get; set;}
        public string ActualAddress { get; set; }
        public string RegisteredAddress { get; set;}
        public string eMail { get; set; }
        public string TIN { get; set; }
        public int idPost { get; set; }  
        public int idFirm { get; set; }
        public string PassportNumbers { get; set; }
        public DateTime PassportIssued { get; set; }    
        public DateTime DateOfEmloyment { get; set; }
    }
}
