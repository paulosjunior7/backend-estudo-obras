﻿namespace Obras.Business.GroupDomain.Models
{
    public class GroupModel
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public int? CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
