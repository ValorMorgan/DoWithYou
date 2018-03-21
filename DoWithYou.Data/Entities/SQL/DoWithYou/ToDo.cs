using DoWithYou.Data.Entities.Base;
using DoWithYou.Data.Interface.Entity.SQL;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;

namespace DoWithYou.Data.Entities.SQL.DoWithYou
{
    public class ToDo : BaseEntity, IToDo
    {
        public long ToDoID { get; set; }

        public string Name { get; set; }

        public bool Complete { get; set; }

        public virtual ToDoList ToDoList { get; set; }

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IToDo))
                return false;

            return GetHashCode() == ((ToDoList)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = nameof(ToDo).GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + base.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + ToDoID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Name);
            hashCode = hashCode * HashConstants.MULTIPLIER + Complete.GetHashCode();
            return hashCode;
        }
    }
}
