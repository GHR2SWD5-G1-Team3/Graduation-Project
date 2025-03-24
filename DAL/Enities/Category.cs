using DAL.Shared;

namespace DAL.Enities
{
    public class Category(string? name, string? description, string? image) : IEditable,IDeletable
    {
        public int Id { get; set; }
        public string? Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public string? Image { get; set; } = image;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public List<SubCategory>? SubCategories { get; set; }

        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, Dictionary<string, object> updatedProperties)
        {
            if (user == null) return false;

            var properties = this.GetType().GetProperties();
            foreach (var property in updatedProperties)
            {
                var propInfo = properties.FirstOrDefault(p => p.Name == property.Key && p.CanWrite);
                if (propInfo != null)
                {
                    propInfo.SetValue(this, Convert.ChangeType(property.Value, propInfo.PropertyType));
                }
            }

            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
