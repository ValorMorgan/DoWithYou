using System.Collections.Generic;
using DoWithYou.Data.Entities.Base;
using DoWithYou.Data.Interface.Entity.SQL;
using DoWithYou.Shared.Constants;

namespace DoWithYou.Data.Entities.SQL.DoWithYou
{
    public class ToDoList : BaseEntity, IToDoList
    {
        public long ToDoListID { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }

        public virtual User User { get; set; }

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IToDoList))
                return false;

            return GetHashCode() == ((ToDoList)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = nameof(ToDoList).GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + base.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + ToDoListID.GetHashCode();
            return hashCode;
        }
    }
}
