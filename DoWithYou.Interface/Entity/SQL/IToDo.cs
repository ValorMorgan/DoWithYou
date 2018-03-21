namespace DoWithYou.Data.Interface.Entity.SQL
{
    public interface IToDo
    {
        bool Complete { get; set; }

        string Name { get; set; }

        long ToDoID { get; set; }
    }
}