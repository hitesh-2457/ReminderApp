using System;

namespace ReminderApi.Model{
    public class BaseEntity{
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}