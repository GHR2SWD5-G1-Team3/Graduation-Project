﻿using static System.Net.Mime.MediaTypeNames;

namespace DAL.Enities
{
    public class Cart(long userId)
    {
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }= DateTime.Now;
        public bool IsChecked { get; private set; }= false;
        public bool IsDeleted { get; private set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; private set; } = userId;
        public User? User { get;  set; }
        public List<CartDetails>? CartProducts { get; set; }
        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user,long userId)
        {
            if (user == null) return false;
            UserId = userId;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }


    }
}
