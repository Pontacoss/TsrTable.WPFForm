namespace TsrTable.Domain.Entities
{
    public class ParameterEntity
    {
        private static int counter = 0;
        public int Id { get; }
        public string Name { get; }
        public ParameterEntity(string name)
        {
            Id = counter;
            Name = name;
            counter++;
        }
    }
}
