using System.Collections.Generic;

namespace WebStore.PocoModels
{
    public class UserRole
    {
        public UserRole()
        {
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanEditItem { get; set; }
        public bool CanEditItemCategory { get; set; }
        public bool CanEditPaymentMethod { get; set; }
        public bool CanIssueOrder { get; set; }
        public bool CanApproveOrder { get; set; }
        public bool CanManageUser { get; set; }

        public ICollection<User> Users { get; set; }
    }
}